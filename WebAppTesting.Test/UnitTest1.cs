using System;
using System.IO;
using OpenQA.Selenium.Chrome;
using Protractor;
using WebAppTesting.Test.Infrastructure;
using WebAppTesting.Web;
using Xunit;

namespace WebAppTesting.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            using var webApplicationFactory = new SeleniumServerFactory<Startup>();
            using var client = webApplicationFactory.CreateClient();

            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            using var driver = new ChromeDriver(Path.GetFullPath("."), options);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            using var ngDriver = new NgWebDriver(driver);

            ngDriver.Url = webApplicationFactory.RootUri;
        }
    }
}
