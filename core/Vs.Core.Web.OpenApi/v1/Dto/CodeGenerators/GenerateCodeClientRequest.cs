using System;

namespace Vs.Core.Web.OpenApi.v1.Dto.CodeGenerators
{
    public abstract class GenerateCodeClientRequest
    {
        public Uri SwaggerContractEndpoint { get; set; }
    }
}