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
    public class MultiplierValidation : Should<ProductClient>, IClassFixture<PactFixture>
    {
        private readonly string Path = "/api/multiply";

        public MultiplierValidation(PactFixture pactFixture) :
            base(pactFixture, "Squarer", "Multiplier")
        {

        }

        [Fact]
        public async Task ValidateMultiplicationContract()
        {
            mockProviderService.Given("A request for a number")
                .UponReceiving("A message")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = Path,
                    Query = "left=7&right=6"
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
                        Total = 42
                    })
                });

            await this.consumer.GetMultiplication(7, 6);

            mockProviderService.VerifyInteractions();
        }

        protected override ProductClient CreateConsumer()
        {
            return new ProductClient(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
        }
    }
}
