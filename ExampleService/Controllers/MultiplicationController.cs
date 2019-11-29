using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ExampleService.Controllers
{
    [ApiController]
    [Route("api/multiply")]
    public class MultiplicationController : ControllerBase
    {
        private readonly ILogger<MultiplicationController> _logger;

        public MultiplicationController(ILogger<MultiplicationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetResult(int left, int right)
        {
            return JsonConvert.SerializeObject(new NumberResponse
            {
                Total = left * right
            });
        }
    }
}
