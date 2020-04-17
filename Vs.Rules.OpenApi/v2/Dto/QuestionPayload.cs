using System.Collections.Generic;

namespace Vs.Rules.OpenApi.v2.Dto
{
    public class QuestionPayload
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