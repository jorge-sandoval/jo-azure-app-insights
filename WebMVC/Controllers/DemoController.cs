using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly string? url;
        private readonly IConfiguration _configuration;

        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
            url = _configuration.GetValue<string>("FunctionApp:HTTP_Trigger");
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            if (String.IsNullOrEmpty(url)) {
                return new OkObjectResult("Hello World2!");
            }
            
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var text = await response.Content.ReadAsStringAsync();

            return new OkObjectResult(text);
        }

    }
}
