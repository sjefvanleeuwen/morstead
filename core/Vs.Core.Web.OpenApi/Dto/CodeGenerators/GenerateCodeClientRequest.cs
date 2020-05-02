using System;

namespace Vs.Core.Web.OpenApi.Dto.CodeGenerators
{
    public abstract class GenerateCodeClientRequest
    {
        public Uri SwaggerContractEndpoint { get; set; }
    }
}