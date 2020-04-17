using System.Collections.Generic;

namespace Vs.Rules.OpenApi.v2.Dto
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