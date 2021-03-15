using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppTesting.Web;
using WebAppTesting.Web.Models;

namespace WebAppTesting.Test.Infrastructure
{
    public sealed class SeleniumServerFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
    {
        private IWebHost? _host;
        public SeleniumServerFactory()
        {
            RootUri = string.Empty;
            ClientOptions.BaseAddress = new Uri("https://localhost");
        }

        public string RootUri { get; private set; }

        public IWebHost Host =>
            _host ?? throw new InvalidOperationException("Server must be initialized before accessing host.");
        
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");
            _host = builder.Build();
            _host.Start();
            RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.Last();

            return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<WeatherContext>(options =>
                    options.UseSqlite(Constants.TestDBConnectString));
            });

            builder.ConfigureTestServices(services =>
            {
                /*
                 * Remove and replace services that need test implementations.
                 *
                 * services.RemoveAll<IEmailService>();
                 * services.AddSingleton<IEmailService, TestEmailService>();
                 */
            });
        }

        protected override IHostBuilder? CreateHostBuilder() => null;

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>()).UseStartup<Startup>();
            return builder;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _host?.Dispose();
            }
        }
    }
}
