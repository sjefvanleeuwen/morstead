using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v2.Controllers
{
    public class QuestionPayload
    {
        IEnumerable<Parameter> Parameters { get; set; }

        public class Parameter
        {
            public string SemanticKey { get; set; }
        }
    }
}