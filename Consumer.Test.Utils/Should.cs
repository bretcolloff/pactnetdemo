using PactNet.Mocks.MockHttpService;

namespace Consumer.Test.Utils
{
    public abstract class Should<TConsumer>
    {
        protected readonly TConsumer consumer;
        protected readonly PactFixture pactFixture;
        protected abstract TConsumer CreateConsumer();

        protected IMockProviderService mockProviderService => pactFixture.MockProviderService;

        public Should(
            PactFixture pactFixture,
            string consumerName,
            string providerName,
            string clientVersion = "1.0.0")
        {
            this.pactFixture = pactFixture;
            this.pactFixture.SetUp(
                consumerName,
                providerName,
                clientVersion);

            mockProviderService.ClearInteractions();
            consumer = CreateConsumer();
        }
    }
}
