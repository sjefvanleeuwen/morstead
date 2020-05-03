using System;

namespace Vs.Core.Web.OpenApi.v1.Dto.CodeGenerators
{
    public class GenerateCSharpClientRequest : GenerateCodeClientRequest
    {
        public Uri Endpoint { get; set; }
    }
}
