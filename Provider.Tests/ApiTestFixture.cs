using ExampleService;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Provider.Tests
{
    class ApiTestFixture : IDisposable
    {
        private IWebHost host;

        public ApiTestFixture()
        {
            var builder =
                new WebHostBuilder()
                   .UseEnvironment("Development")
                   .UseKestrel()
                   .UseUrls("http://localhost:1010/")
                   .UseStartup<Startup>();

            host = builder.Build();
            host.Start();
        }

        public void Dispose()
        {
            host.StopAsync();
        }
    }
}
