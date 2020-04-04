using Microsoft.AspNetCore.Mvc;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public interface IServiceController
    {
        IExecutionResult Execute([FromBody] IExecuteRequest executeRequest);
        IParseResult Parse([FromBody] IParseRequest parseRequest);
    }
}