using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace MultiplicationService.Controllers
{
    [ApiController]
    [Route("api/add")]
    public class AdditionController : ControllerBase
    {
        private readonly ILogger<AdditionController> _logger;

        public AdditionController(ILogger<AdditionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetResult(int left, int right)
        {
            return JsonConvert.SerializeObject(new NumberResponse
            {
                Total = left + right
            });
        }
    }
}
