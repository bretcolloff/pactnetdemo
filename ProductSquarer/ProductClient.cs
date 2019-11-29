using Common.Http;
using Messages;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProductSquarer
{
    public class ProductClient
    {
        private readonly IRestProxy RestProxy;
        private readonly string BaseUri;

        public ProductClient(IRestProxy restProxy, string baseUri)
        {
            this.RestProxy = restProxy;
            this.BaseUri = baseUri + "/api/multiply";
        }

        public async Task<NumberResponse> GetMultiplication(int left, int right)
        {
            var response = await RestProxy.GetStringAsync($"{BaseUri}?left={left}&right={right}");
            return JsonConvert.DeserializeObject<NumberResponse>(response);
        }
    }
}
