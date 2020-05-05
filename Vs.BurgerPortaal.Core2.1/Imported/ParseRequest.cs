using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public class ParseRequest : IParseRequest
    {
        public string Config { get; set; }
    }
}