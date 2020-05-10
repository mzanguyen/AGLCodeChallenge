using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AGLChallenge.Client.IntegrationTest
{
    public class IndexPageTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IndexPageTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        public async Task OnGet_ShouldReturnPetList(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await HtmlHelpers.GetDocumentAsync(response);
            var resultElement = content.QuerySelector("#result");

            var gender = resultElement.ChildElementCount;
            var femaleWithCat = resultElement.FirstElementChild.FirstElementChild.ChildElementCount;

            // Assert, assuming that AGL webservice won't change its data
            Assert.Equal(2, gender);
            Assert.Equal(3, femaleWithCat);
            
        }
    }
}
