using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers
{
    public class ParseRequest : IParseRequest
    {
        public string Config { get; set; }
    }
}