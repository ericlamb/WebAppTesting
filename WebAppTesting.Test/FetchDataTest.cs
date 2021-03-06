using System;
using System.Linq;
using WebAppTesting.Test.Pages;
using WebAppTesting.Web.Models;
using Xunit;

namespace WebAppTesting.Test
{
    public class FetchDataTest : SeleniumTest
    {
        [Fact]
        public void WeatherForecastTests()
        {
            InitializeData();
            var homePage = new HomePage(NgDriver);
            var fetchDataPage = homePage.OpenFetchData();

            TestLoadedData(fetchDataPage);
            TestAdd(fetchDataPage);
            TestDelete(fetchDataPage);
        }

        private void InitializeData()
        {
            using var context = Context;

            context.Forecasts.AddRange(
                new WeatherForecastEntity()
                {
                    Date = DateTime.Now.AddDays(-2),
                    TemperatureC = 10,
                    Summary = "Cool",
                },
                new WeatherForecastEntity()
                {
                    Date = DateTime.Now.AddDays(-1),
                    TemperatureC = 100,
                    Summary = "Hot",
                }
            );

            context.SaveChanges();
        }

        private static void TestLoadedData(FetchDataPage fetchDataPage)
        {
            Assert.Equal(2, fetchDataPage.Forcasts.Count());
            Assert.All(fetchDataPage.Forcasts, x => Assert.True(x.TempC > 0));
            Assert.All(fetchDataPage.Forcasts, x => Assert.True(!string.IsNullOrWhiteSpace(x.Summary)));
        }

        private void TestAdd(FetchDataPage fetchDataPage)
        {
            fetchDataPage.NewForcast.Date = DateTime.Now;
            fetchDataPage.NewForcast.TempC = 20;
            fetchDataPage.NewForcast.Summary = "Balmy";
            fetchDataPage.NewForcast.Submit();

            Assert.Equal(3, fetchDataPage.Forcasts.Count());
            Assert.True(fetchDataPage.Forcasts.Any(x => x.Summary == "Balmy"));
            using (var context = Context)
            {
                Assert.Equal(3, context.Forecasts.Count());
            }
        }

        private void TestDelete(FetchDataPage fetchDataPage)
        {
            fetchDataPage.Forcasts.First().Delete();
            Assert.Equal(2, fetchDataPage.Forcasts.Count());
            using (var context = Context)
            {
                Assert.Equal(2, context.Forecasts.Count());
            }
        }
    }
}
