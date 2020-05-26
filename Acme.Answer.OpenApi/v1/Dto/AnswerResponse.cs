using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public class AnswerResponse
    {
        public IEnumerable<Parameter> Parameters { get; set; }

        public class Parameter : IParameter
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public interface IParameter
        {
            string Name { get; set; }
            string Value { get; set; }
        }
    }
}