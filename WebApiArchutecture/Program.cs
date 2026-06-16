using AutoMapper;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using WebApiArchitecture.Application.Features.Tattoos.Commands.DeleteOldSignUps;
using WebApiArchitecture.Common;
using WebApiArchitecture.Infrastructure.Services.Email;
using WebApiArchitecture.Infrastructure.Services.Pdf;
using WebApiArchutecture.Application;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;
using WebApiArchutecture.Application.Mappings;
using WebApiArchutecture.Application.Validators;
using WebApiArchutecture.Infrastructure;
using WebApiArchutecture.Infrastructure.Repositories.IArtistRepository;
using WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.TattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.UserRepository;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;
using WebApiArchutecture.Infrastructure.UnitOfWork;
using WebApiArchutecture.Middlewares;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


if (builder.Environment.IsEnvironment("Testing"))
{
  
}
else
{
    builder.Services.AddHangfire(config => config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddHangfireServer();
}


builder.Services.AddControllers().AddJsonOptions(options =>
{

    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
    options.InstanceName = "Ecommerce_";
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminTelegram", policy => policy.RequireRole("Admin").RequireClaim("Telegram_Tag").RequireAssertion(context => context.User.HasClaim(c => c.Type == "Telegram_Tag"
    && c.Value.StartsWith("@"))));
});
var emailConfig = builder.Configuration.GetSection("EmailSettings");


var smtpServer = emailConfig["SmtpServer"] ?? "localhost";
var fromEmail = emailConfig["From"] ?? "noreply@yourdomain.com";


var port = int.TryParse(emailConfig["Port"], out var parsedPort) && parsedPort > 0
           ? parsedPort
           : 587;

builder.Services
    .AddFluentEmail(fromEmail)
    .AddSmtpSender(new System.Net.Mail.SmtpClient(smtpServer)
    {
        Port = port,
        Credentials = new System.Net.NetworkCredential(
            emailConfig["Username"] ?? string.Empty,
            emailConfig["Password"] ?? string.Empty
        ),
        EnableSsl = true
    });


var app = builder.Build();



app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.UseHangfireDashboard();

if (app.Environment.EnvironmentName != "Testing")
{
    using (var scope = app.Services.CreateScope())
    {
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        recurringJobManager.AddOrUpdate(
            "cleanup-signups",
            () => mediator.Send(new DeleteOldSignUpsCommand()),
            Cron.Daily
        );
    }
}


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine("DEBUG: Починаю перевірку міграцій...");

    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("DEBUG: Міграції успішно застосовані!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DEBUG: ПОМИЛКА ПРИ МІГРАЦІЇ: {ex.Message}");
        Console.WriteLine($"DEBUG: СТЕК: {ex.StackTrace}");
    }
}

app.Run();



public partial class Program { }
