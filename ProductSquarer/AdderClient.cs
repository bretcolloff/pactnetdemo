using Common.Http;
using Messages;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProductSquarer
{
    public class AdderClient
    {
        private readonly IRestProxy RestProxy;
        private readonly string BaseUri;

        public AdderClient(IRestProxy restProxy, string baseUri)
        {
            this.RestProxy = restProxy;
            this.BaseUri = baseUri + "/api/add";
        }

        public async Task<NumberResponse> GetAddition(int left, int right)
        {
            var response = await RestProxy.GetStringAsync($"{BaseUri}?left={left}&right={right}");
            return JsonConvert.DeserializeObject<NumberResponse>(response);
        }
    }
}
