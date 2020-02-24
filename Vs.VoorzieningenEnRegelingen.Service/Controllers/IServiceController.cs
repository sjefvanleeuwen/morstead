using Microsoft.AspNetCore.Mvc;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public interface IServiceController
    {
        ExecutionResult Execute([FromBody] ExecuteRequest executeRequest);
        ParseResult Parse([FromBody] ParseRequest parseRequest);
    }
}