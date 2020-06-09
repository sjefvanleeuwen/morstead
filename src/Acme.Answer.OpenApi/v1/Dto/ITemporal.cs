using Acme.Answer.OpenApi.v1.Features.Feature1;

namespace Acme.Answer.OpenApi.v1.Dto
{
    internal interface ITemporal
    {
        public TimeRange TimeRange { get; set; }
    }
}