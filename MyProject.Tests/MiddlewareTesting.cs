using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WebApiArchutecture.Application.DTOs.UserDto;
using Xunit;
namespace MyProject.Tests
{
    public class MiddlewareTesting : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MiddlewareTesting (CustomWebApplicationFactory<Program> factory) { _client = factory.CreateClient (); }


        [Fact]
        public async Task Middleware_WhenExceptionThrown_ShouldReturn500_AndJsonErrorStructure()
        {
        
            var invalidUrl = "api/test-exception";

           


         
            var response = await _client.GetAsync(invalidUrl);

          
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            var errorResult = await response.Content.ReadFromJsonAsync<ErrorResponseSample>();

            errorResult.Should().NotBeNull();
            errorResult.Status.Should().Be(500);
            errorResult.Title.Should().NotBeNullOrEmpty();


        }
        }

    public class ErrorResponseSample
    {
        public int Status { get; set; }
        public string Title { get; set; }
    }
}
