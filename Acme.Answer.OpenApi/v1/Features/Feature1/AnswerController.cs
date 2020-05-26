using Acme.Answer.OpenApi.v1.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;
using static Acme.Answer.OpenApi.v1.Dto.AnswerResponse;

namespace Acme.Answer.OpenApi.v1.Features.Feature1
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0-feature1")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("Answer API", Description = "This is current api feature branche")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        /// <summary>
        /// Retrieves the toetsingsinkomen
        /// </summary>
        /// <returns>The toetsingsinkomen</returns>
        /// <response code="200">Parameter provided</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(AnswerResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        [HttpPost("get-value-for-parameter")]
        public async Task<IActionResult> GetByParameterName(string parameterName)
        {
            return await GetAnswerByParameterName(parameterName);
        }

        private async Task<IActionResult> GetAnswerByParameterName(string parameterName)
        {
            try
            {
                AnswerResponse result = null;
                switch (parameterName)
                {
                    case "alleenstaande":
                        result = await GetLivingSituation("alleenstaande");
                        break;
                    case "aanvrager_met_toeslagpartner":
                        result = await GetLivingSituation("alleenstaande");
                        break;
                    case "toetsingsinkomen":
                        result = await GetAssessmentIncome();
                        break;
                    default:
                        return StatusCode(400, $"ParameterName supplied: '{parameterName}' is not a valid parameter.");
                }
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex));
            }
        }

        private async Task<AnswerResponse> GetLivingSituation(string type)
        {
            string value = null;
            switch (type)
            {
                case "alleenstaande":
                    value = "true";
                    break;
                case "aanvrager_met_toeslagpartner":
                    value = "false";
                    break;
                default:
                    throw new Exception($"Unspecified livingsituation requested: '{type}'");
            }
            var answerPayload = new AnswerResponse
            {
                Parameters = new List<Parameter> {
                    new Parameter
                    {
                        Name = type,
                        Value = value
                    }
                }
            };
            return answerPayload;
        }

        private async Task<AnswerResponse> GetAssessmentIncome()
        {
            var answerPayload = new AnswerResponse
            {
                Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "toetsingsinkomen",
                            Value = "24000"
                        }
                    }
            };
            return answerPayload;
        }
    }
}
