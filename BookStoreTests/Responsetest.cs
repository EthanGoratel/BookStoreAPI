using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using BookStoreApi.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace BookStoreTests
{
    public class Responsetest
    {
        [Theory]
        [InlineData("/api/Books")]
        [InlineData("/api/Books/623ed4ced956d6d3d963a226")]

        public async Task GetTest(string url)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            var client = application.CreateClient();
            //...
            var response = await client.GetAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        }

        [Theory]
        [InlineData("/api/Books")]
        public async Task PostTest(string url)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services

                });

            var client = application.CreateClient();
            //...

            var elem = new Book()
            {

                BookName = "Biographie de Jean Milou",
                Price = 3,
                Category = "Roman",
                Author = "Jean Milou"
            };

            var serial = JsonConvert.SerializeObject(elem);
            var stringcontent = new StringContent(serial, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringcontent);


            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);


        }
        [Theory]
        [InlineData("/api/Books")]
        public async Task DeleteTest(string url)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services

                });

            var client = application.CreateClient();
            //...
            var response = await client.DeleteAsync(url+"/1");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);


            var elem2 = new Book()
            {

                BookName = "Biographie de Jean Milou",
                Price = 3,
                Category = "Roman",
                Author = "Jean Milou"
            };
            
            var serial2 = JsonConvert.SerializeObject(elem2);
            var stringcontent2 = new StringContent(serial2, System.Text.Encoding.UTF8, "application/json");
            var responsePost = await client.PostAsync(url, stringcontent2);
            var PostID = JsonConvert.DeserializeObject<Book>(await responsePost.Content.ReadAsStringAsync()).Id;
            var response2 = await client.DeleteAsync(url+"/"+PostID);
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response2.StatusCode);
        
        
        
        }

    }
}
