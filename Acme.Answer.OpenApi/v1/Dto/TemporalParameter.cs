using Acme.Answer.OpenApi.v1.Features.Feature1;
using static Acme.Answer.OpenApi.v1.Dto.AnswerResponse;

namespace Acme.Answer.OpenApi.v1.Dto
{
    public class TemporalParameter : IParameter, ITemporal
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}