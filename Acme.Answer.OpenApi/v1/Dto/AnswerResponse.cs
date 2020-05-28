using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public partial class AnswerResponse
    {
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}