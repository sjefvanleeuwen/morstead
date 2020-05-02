using System;

namespace Vs.Rules.OpenApi.v1.Dto
{
    public abstract class GenerateCodeClientRequest
    {
        public Uri SwaggerContractEndpoint { get; set; }
    }
}