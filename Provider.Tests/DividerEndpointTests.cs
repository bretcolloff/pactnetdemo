using Provider.Tests.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Provider.Tests
{
    public class DividerEndpointTests : IClassFixture<ApiTestFixture>
    {
        private const string ServerUri = "http://localhost:1010";
        private const string MiddlewareUri = "http://localhost:1011";
        private const string ServiceName = "Divider";
        private readonly PactVerifierConfig config;

        public DividerEndpointTests(ITestOutputHelper output)
        {
            this.config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> { new XUnitOutput(output) },
                PublishVerificationResults = true,
                ProviderVersion = "1.0.0"
            };
        }

        private void VerifyContractWithConsumer(string consumerName)
        {
            using (var host = WebHost.CreateDefaultBuilder()
            .UseUrls(MiddlewareUri)
            .UseStartup<TestStartup>()
            .Build())
            {
                host.Start();

                IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier
                    .ProviderState($"{MiddlewareUri}/provider-states")
                    .ServiceProvider(ServiceName, ServerUri)
                    .HonoursPactWith(consumerName)
                    .PactUri($"http://localhost/pacts/provider/{ ServiceName }/consumer/{ consumerName }/latest")
                    .Verify();
            }
        }

        [Fact]
        public void ValidateContractWithReader()
        {
            VerifyContractWithConsumer("Squarer");
        }
    }
}
