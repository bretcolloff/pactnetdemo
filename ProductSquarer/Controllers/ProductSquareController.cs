using Common.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductSquarer.Controllers
{
    [ApiController]
    [Route("api/squareproduct")]
    public class ProductSquareController : ControllerBase
    {
        private readonly ILogger<ProductSquareController> _logger;
        private string ProductClientUri = "https://www.examplewebsite.com/productendpoint";

        public ProductSquareController(ILogger<ProductSquareController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(int left, int right)
        {
            // Call out to multiplying service.
            var productClient = new ProductClient(new RestProxy(), ProductClientUri);
            var product = productClient.GetMultiplication(left, right).Result;

            // Square result.
            return (product.Total * product.Total).ToString();
        }
    }
}
