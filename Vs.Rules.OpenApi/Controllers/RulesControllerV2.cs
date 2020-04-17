using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;
using Vs.Rules.OpenApi.Dto.v2;

namespace Vs.Rules.OpenApi.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("2020-04-17.2.0.alpha")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current api version")]
    [ApiController]
    public class RulesControllerV2 : ControllerBase
    {
        /// <summary>
        /// Pings the Rules engine
        /// </summary>
        /// <returns>Pong</returns>
        [HttpGet]
        public async Task<string> Ping()
        {
            return "Pong from v2";
        }

        /// <summary>
        /// Gets the statistics for the current configuration being executed.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Statistics</returns>
        /// <response code="404">Some yaml resource(s) could not be found</response>
        /// <response code="400">Configuration Invalid</response>
        [HttpPost("statistics")]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 404)]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 400)]
        public async Task<RuleStatistics> GetStatistics(RuleConfiguration configuration)
        {
            return new RuleStatistics();
        }
        /// <summary>
        /// For debugging purposes you can parse the configuration without executing it.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>ParesResult</returns>
        /// <response code="404">Some yaml resource(s) could not be found</response>
        /// <response code="400">Configuration Invalid</response>
        [HttpPost("parse")]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 404)]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 400)]
        public async Task<ParseResult> GetParseResults(RuleConfiguration configuration)
        {
            return new ParseResult();
        }
    }
}
