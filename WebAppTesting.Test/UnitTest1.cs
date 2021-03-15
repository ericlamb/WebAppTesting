using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;
using WebAppTesting.Test.Infrastructure;
using WebAppTesting.Test.Pages;
using WebAppTesting.Web;
using Xunit;

namespace WebAppTesting.Test
{
    public class UnitTest1
    {
        [Fact]
        public void WhenCounterIsIncremented()
        {
            using var webApplicationFactory = new SeleniumServerFactory<Startup>();
            using var client = webApplicationFactory.CreateClient();

            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            using var driver = new ChromeDriver(Path.GetFullPath("."), options);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            using var ngDriver = new NgWebDriver(driver);

            ngDriver.Url = webApplicationFactory.RootUri;
            ngDriver.WaitForAngular();

            var homePage = new HomePage(ngDriver);
            var countPage = homePage.OpenCountPage();

            var incrementButton =
                ngDriver.FindElement(By.XPath("//app-counter-component/button"));
            incrementButton.Click();
            ngDriver.WaitForAngular();

            var currentCount =
                ngDriver.FindElement(By.XPath("//app-counter-component")).FindElement(By.ClassName("currentCount"));

            Assert.Equal("1", currentCount.Text);
        }
    }
}
