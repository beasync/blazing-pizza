using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazingPizza.Api.FunctionalTests
{
    public class BlazingPizzaApiShould
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public BlazingPizzaApiShould()
        {
            server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());

            client = server.CreateClient();
        }

        [Fact]
        public async Task AllowGetAllProducts()
        {
            var response = await client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var expected = @"[{""id"":1,""name"":""Margherita"",""price"":12.60},{""id"":2,""name"":""Diavola"",""price"":13.60}]";

            Assert.Equal(expected, responseString);
        }

        [Fact]
        public async Task AllowGetFilteredProducts()
        {
            var response = await client.GetAsync("/api/products?name=Margherita");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var expected = @"[{""id"":1,""name"":""Margherita"",""price"":12.60}]";

            Assert.Equal(expected, responseString);
        }

        [Fact]
        public async Task AllowCreateProducts()
        {
            var payload = @"{ ""name"": ""Capricciosa"", ""price"": 12.60 }";
            var response = await client.PostAsync("/api/products", new StringContent(payload, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var expected = @"{""id"":3,""name"":""Capricciosa"",""price"":12.60}";

            Assert.Equal(expected, responseString);
        }

        [Fact]
        public async Task AllowRemoveProducts()
        {
            var response = await client.DeleteAsync("/api/products/1");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
