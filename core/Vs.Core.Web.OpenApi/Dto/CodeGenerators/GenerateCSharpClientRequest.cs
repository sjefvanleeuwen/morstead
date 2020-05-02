using System;

namespace Vs.Core.Web.OpenApi.Dto.CodeGenerators
{
    public class GenerateCSharpClientRequest : GenerateCodeClientRequest
    {
        public Uri Endpoint { get; set; }
    }
}
