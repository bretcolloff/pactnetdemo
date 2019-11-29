using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace MultiplicationService.Controllers
{
    [ApiController]
    [Route("api/divide")]
    public class DivisionController : ControllerBase
    {
        private readonly ILogger<DivisionController> _logger;

        public DivisionController(ILogger<DivisionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetResult(int left, int right)
        {
            return JsonConvert.SerializeObject(new NumberResponse
            {
                Total = left / right
            });
        }
    }
}
