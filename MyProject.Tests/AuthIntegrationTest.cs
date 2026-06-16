using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features.Auth.Commands.Loggin;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MyProject.Tests
{
    public class AuthIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>> 
    {
        private readonly HttpClient _client;
        public AuthIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("TestScheme");
        }


        [Fact]
        public async Task TestRegisterWithValidData_ShouldReturn200OK()
        {
            var registerDto = new RegisterUserCommand
            (
               "TattooMasterLviv",
                 "Password112121@23!",
                 "MasterLviv@gmail.com",
                 "Password112121@23!",
                "@master_lviv",
                "User"
            );



            var response = await _client.PostAsJsonAsync("api/auth/register", registerDto);


            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
               
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new Exception($"Сервер повернув 500. Опис помилки: {errorContent}");
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


       
    }
}
