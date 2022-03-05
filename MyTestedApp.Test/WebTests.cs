using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Testing;

using MyTestedAspNetCoreApp;

using Xunit;

namespace MyTestedApp.Test
{
    public class WebTests
    {
        [Fact]
        public async Task HomePageShouldHaveWelcomeHeading()
        {
            var webApplicationFactory = new WebApplicationFactory<Startup>();

            HttpClient client = webApplicationFactory.CreateClient();

            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<h2>Welcome in my app!!!</h2>", html);

            Assert.True(response.Headers.Contains("x-info-action-name"));
        }
    }
}
