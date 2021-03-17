using System;
using System.Data;
using System.IO;
using System.Net.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Chrome;
using Protractor;
using WebAppTesting.Test.Infrastructure;
using WebAppTesting.Web;
using WebAppTesting.Web.Models;

namespace WebAppTesting.Test
{
    public abstract class SeleniumTest : IDisposable
    {
        public SeleniumTest()
        {
            DbOptions = new DbContextOptionsBuilder<WeatherContext>()
                .UseSqlite(Constants.TestDBConnectString)
                .Options;
            Connection = InitializeDatabase();

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

        public NgWebDriver NgDriver { get; }

        public HttpClient Client { get; }

        private IDbConnection Connection { get; }

        public SeleniumServerFactory<Startup> ServerFactory { get; }

        public WeatherContext Context => new WeatherContext(DbOptions);

        public DbContextOptions<WeatherContext> DbOptions { get; }

        public IDbConnection InitializeDatabase()
        {
            var connection = new SqliteConnection(Constants.TestDBConnectString);
            connection.Open();

            using (var context = new WeatherContext(DbOptions))
            {
                context.Database.Migrate();
            }

            return connection;
        }

        public void Dispose()
        {
            Driver.Dispose();
            NgDriver.Dispose();
            Client.Dispose();
            ServerFactory.Dispose();
            Connection.Dispose();
        }
    }
}
