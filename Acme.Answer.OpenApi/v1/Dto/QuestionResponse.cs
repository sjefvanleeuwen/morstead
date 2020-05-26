using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public class QuestionResponse
    {
        public IEnumerable<Parameter> Parameters { get; set; }

        public class Parameter
        {
            public string SemanticKey { get; set; }
        }
    }
}