using Common.Http;
using Consumer.Test.Utils;
using Messages;
using Newtonsoft.Json;
using PactNet.Mocks.MockHttpService.Models;
using ProductSquarer;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Consumer.Tests
{
    public class AdderValidation : Should<AdderClient>, IClassFixture<PactFixture>
    {
        private readonly string Path = "/api/add";

        public AdderValidation(PactFixture pactFixture) :
            base(pactFixture, "Squarer", "Adder")
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
                    Query = "left=4&right=6"
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
                        Total = 10
                    })
                });

            await this.consumer.GetAddition(4, 6);

            mockProviderService.VerifyInteractions();
        }

        protected override AdderClient CreateConsumer()
        {
            return new AdderClient(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
        }
    }
}
