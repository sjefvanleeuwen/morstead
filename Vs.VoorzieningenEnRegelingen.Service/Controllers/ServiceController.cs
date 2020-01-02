using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ParseResult Parse(string config)
        {
            var controller = new YamlScriptController();
            return controller.Parse(config);
        }
    }
}
