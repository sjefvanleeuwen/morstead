using System;

namespace Vs.Core.Web.OpenApi.v1.Dto.CodeGenerators
{
    public abstract class GenerateCodeRequest
    {
        public Uri SwaggerContractEndpoint { get; set; }
    }
}