using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;
using WebApiArchutecture.Application.Features.Tattoos.Queries.GetArtistsWithPrice;
using WebApiArchutecture.Application.Features.Tattoos.Queries.GetTattoos;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure;

namespace MyProject.Tests
{
    public class TattoosIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        public TattoosIntegrationTest (CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
            _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("TestScheme");
        }
        private async Task ClearDatabase(AppDbContext db)
        {
            db.Artists.RemoveRange(db.Artists);
            db.Tattoos.RemoveRange(db.Tattoos);
            db.SignUps.RemoveRange(db.SignUps);
            await db.SaveChangesAsync();
        }


        [Fact]
        public async Task GetTattoo_ShouldReturn200OK()
        {
            var url = "api/tattoos?PageNumber=1&PageSize=5";

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Tattoos.Add(new Tattoo { Id = 1, Color = Color.multicolored , DateOfCreating = DateTime.UtcNow , Price = 200 , Size = "10-20" , Style = "japan" , RowVersion = [1 , 2] });
                await db.SaveChangesAsync();
            }

            var response = await _client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Сервер впав з 500! Ось що він каже: {content}");
            }

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task GetArtistsByPrice_ShouldReturn200OK()
        {
            var baseUrl = "api/tattoos/artists-by-price";

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Artists.Add(new Artist { Id = 1, Age = 22, Experience = 1, Name = "Viktor", PriceForSession = 100, Surname = "Otenko", RowVersion = [1, 2] });
                await db.SaveChangesAsync();
            }

            var queryParams = new Dictionary<string, string?>
    {
       
        { "Params.PageNumber", "1" },
        { "Params.PageSize", "1" },
        { "price", "100" }
    };

            

            var url = QueryHelpers.AddQueryString(baseUrl, queryParams);
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }



        [Fact]
        public async Task GetSignUps_ShouldReturn200OK()
        {
            var baseUrl = "api/tattoos/ShowSignups";

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.SignUps.Add(new SignUpForTattoo { Id = 1 , ArtistId = 1 , Artist = new Artist { Age = 18 , Experience = 2 , Id = 1 , Name = "Dimon" , Surname = "Pokemon" , PriceForSession = 200 , RowVersion = [1,2] } , NumberOfClient = "+380937356622" , Sessions = 2 , TimeOfSign = DateTime.UtcNow.AddDays(2) ,  RowVersion = [1, 2] });
                await db.SaveChangesAsync();
            }

            var queryParams = new Dictionary<string, string?>
    {

        { "Params.PageNumber", "1" },
        { "Params.PageSize", "1" }
    };
            var url = QueryHelpers.AddQueryString(baseUrl, queryParams);
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task CreateArtist_ShouldReturn200OK_And_CorrectlySave()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await ClearDatabase(db);


                var url = "api/tattoos/create-artist";

                var artist = new CreateArtistCommand
                (
                    "Sergii",
                    "Xanax",
                     22,
                     100,
                     3
                );

                var response = await _client.PostAsJsonAsync(url, artist);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    throw new Exception($"Сервер повернув 500. Опис помилки: {content}");
                }

                

                    var createdArtist = db.Artists.FirstOrDefault(p => p.Name == "Sergii");

                    createdArtist.Should().NotBeNull();
                    createdArtist.Age.Should().Be(22);
                


                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);




            }
        }

        [Fact]
        public async Task CreateSignUp_ShouldReturn200OK_And_CorrectlySave()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await ClearDatabase(db);


                var artist = new CreateArtistCommand
                (
                    "Sergii",
                    "Xanax",
                     22,
                     100,
                     3
                );

                var forArtist = await _client.PostAsJsonAsync("api/tattoos/create-artist", artist);


                


                var url = "api/tattoos/registration";

                var signup = new CreateSignUpForCommand
                  (
                    "+380937356622",
                    DateTime.UtcNow.AddDays(20),
                    10,
                    1
                  );

                var response = await _client.PostAsJsonAsync(url , signup);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    
                    System.Diagnostics.Debug.WriteLine(errorDetails);
                    throw new Exception($" Ось що каже сервер: {errorDetails}");
                } 

                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                var createdSignUp = db.SignUps.FirstOrDefault(p => p.NumberOfClient == "+380937356622");
                createdSignUp.Should().NotBeNull();
                createdSignUp.NumberOfClient.Should().Be("+380937356622");
            }
        }

        [Fact]
        public async Task CreateTattoo_ShouldReturn200OK_And_CorrectlySave()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await ClearDatabase(db);

                var url = "api/tattoos/create-tattoo";

                var tattoo = new CreateTattooCommand("10-20" , Color.multicolored , "Old-School" , 75);

                var response = await _client.PostAsJsonAsync(url , tattoo);

                var createdTattoo = db.Tattoos.FirstOrDefault(p=> p.Price == 75);

                createdTattoo.Should().NotBeNull();
                createdTattoo.Style.Should().Be("Old-School");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }


        [Fact]
        public async Task DeleteSignUpById_ShouldReturnNoContent() 
        {
            using (var scope = _factory.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await ClearDatabase(db);


                var artist = new CreateArtistCommand
                (
                    "Sergii",
                    "Xanax",
                     22,
                     100,
                     3
                );

                var responseForArtist = await _client.PostAsJsonAsync("api/tattoos/create-artist", artist);


                var signup = new CreateSignUpForCommand
                  (
                    "+380937356622",
                    DateTime.UtcNow.AddDays(20),
                    10,
                    1
                  );

                var responseForSignUp = await _client.PostAsJsonAsync("api/tattoos/registration", signup);

                var createdDto = await responseForSignUp.Content.ReadFromJsonAsync<SignUpForTattooReadDto>();
                var idToDelete = createdDto.Id;


                var response = await _client.DeleteAsync($"api/tattoos/Delete-SignUp/{idToDelete}");


                var deletedSignUp = db.SignUps.FirstOrDefault(p => p.Id == idToDelete);
                deletedSignUp.Should().BeNull();

                response.StatusCode.Should().Be(HttpStatusCode.NoContent);


            }

        }

    }
}
