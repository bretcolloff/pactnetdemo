using Common.Http;
using Consumer.Test.Utils;
using Messages;
using Newtonsoft.Json;
using PactNet.Mocks.MockHttpService.Models;
using ProductSquarer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consumer.Tests
{
    public class DividerTests : Should<DividerClient>, IClassFixture<PactFixture>
    {
        private readonly string Path = "/api/divide";

        public DividerTests(PactFixture pactFixture) :
            base(pactFixture, "Squarer", "Divider")
        {

        }

        [Fact]
        public async Task ValidationAdditionContract()
        {
            mockProviderService.Given("A request for a number")
                .UponReceiving("A message")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = Path,
                    Query = "left=8&right=2"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "text/plain; charset=utf-8" }
                    },
                    Body = JsonConvert.SerializeObject(new NumberResponse
                    {
                        Total = 4
                    })
                });

            await this.consumer.GetDivision(8, 2);

            mockProviderService.VerifyInteractions();
        }

        protected override DividerClient CreateConsumer()
        {
            return new DividerClient(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
        }
    }
}
