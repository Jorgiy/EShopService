using System;
using System.Net.Http;
using EShopService.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace EShopService.Tests
{
    public class TestContext : IDisposable
    {
        public HttpClient Client { get; }

        public TestContext()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}