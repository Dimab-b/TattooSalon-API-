using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApiArchitecture.Application.Exceptions;

namespace WebApiArchutecture.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) { _logger = logger; }


        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext , Exception exception , CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Сталася непередбачувана помилка {0}" , exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Помилка валідації даних";
                    problemDetails.Detail = "Одне або кілька полів не пройшли перевірку";

                    problemDetails.Extensions["errors"] = validationException.Errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    break;



                case KeyNotFoundException keyNotFoundException:
                case NotFoundException notFoundException:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Ресурс не знайдено";
                    problemDetails.Detail = exception.Message;

                    break;


                case DbUpdateConcurrencyException dbUpdateConcurrencyException:
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Конфлікт змінни даних";
                    problemDetails.Detail = "Ці дані вже були оновлені іншим користувачем, будь ласка, спробуйте ще раз";

                    break;




                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Внутрішня помилка сервера";
                    problemDetails.Detail = "Помилка на нашій стороні , ми вже фіксимо";

                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
