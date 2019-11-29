using Common.Http;
using Messages;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProductSquarer
{
    public class DividerClient
    {
        private readonly IRestProxy RestProxy;
        private readonly string BaseUri;

        public DividerClient(IRestProxy restProxy, string baseUri)
        {
            this.RestProxy = restProxy;
            this.BaseUri = baseUri + "/api/divide";
        }

        public async Task<NumberResponse> GetDivision(int left, int right)
        {
            var response = await RestProxy.GetStringAsync($"{BaseUri}?left={left}&right={right}");
            return JsonConvert.DeserializeObject<NumberResponse>(response);
        }
    }
}
