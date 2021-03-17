using WebAppTesting.Test.Pages;
using Xunit;

namespace WebAppTesting.Test
{
    public class FetchDataTest : SeleniumTest
    {
        [Fact]
        public void WeatherForecastTests()
        {
            var homePage = new HomePage(NgDriver);
            var fetchDataPage = homePage.OpenFetchData();
        }
    }
}
