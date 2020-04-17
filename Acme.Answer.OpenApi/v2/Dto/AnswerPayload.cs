using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v2.Controllers
{
    public class AnswerPayload
    {
        IEnumerable<IParameter> Parameters { get; set; }

        public class Parameter : IParameter
        {
            public object Value { get; set; }
            public string SemanticKey { get; set; }
        }

        public interface IParameter : ISemanticKey
        {
            object Value { get; set; }
        }

        public interface ISemanticKey
        {
            string SemanticKey { get; set; }
        }
    }
}