using System;
using System.IO;
using System.Net.Http;
using OpenQA.Selenium.Chrome;
using Protractor;
using WebAppTesting.Test.Infrastructure;
using WebAppTesting.Web;

namespace WebAppTesting.Test
{
    public abstract class SeleniumTest : IDisposable
    {
        public SeleniumTest()
        {
            ServerFactory = new SeleniumServerFactory<Startup>();
            Client = ServerFactory.CreateClient();
            
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            Driver = new ChromeDriver(Path.GetFullPath("."), options);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            NgDriver = new NgWebDriver(Driver) { Url = ServerFactory.RootUri };
            NgDriver.WaitForAngular();
        }

        public ChromeDriver Driver { get; }

        public NgWebDriver NgDriver { get;  }

        public HttpClient Client { get; }

        public SeleniumServerFactory<Startup> ServerFactory { get; }

        public void Dispose()
        {
            Driver.Dispose();
            NgDriver.Dispose();
            Client.Dispose();
            ServerFactory.Dispose();
        }
    }
}
