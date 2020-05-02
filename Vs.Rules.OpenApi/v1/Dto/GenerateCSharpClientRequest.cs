using System;

namespace Vs.Rules.OpenApi.v1.Dto
{
    public class GenerateCSharpClientRequest : GenerateCodeClientRequest
    {
        public Uri Endpoint { get; set; }
    }
}
