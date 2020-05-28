using System.Collections.Generic;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public class AnswerTemporalResponse
    {
        public IEnumerable<TemporalParameter> Parameters { get; set; }
    }
}
