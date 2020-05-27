using Acme.Answer.OpenApi.v1.Features.Feature1;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public class AnswerTemporalRequest
    {
        public string ParameterName { get; set; }
        public TimeRangeQuery TimeRangeQuery { get; set; }
    }
}
