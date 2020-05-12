using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v2.Controllers
{
    public class AnswerPayload
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