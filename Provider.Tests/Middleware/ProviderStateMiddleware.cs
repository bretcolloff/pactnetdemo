using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Tests.Middleware
{
    public class ProviderState
    {
        public string Consumer { get; set; }
        public string State { get; set; }
    }

    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "Squarer";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "A request for a number",
                    () => 
                    {
                        Console.WriteLine("A request for a number");    
                    }
                }
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                this.HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(String.Empty);
            }
            else
            {
                await this._next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            try
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                    context.Request.Body != null)
                {
                    string jsonRequestBody = String.Empty;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEndAsync().Result;
                    }

                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                        providerState.Consumer == ConsumerName)
                    {
                        _providerStates[providerState.State].Invoke();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
