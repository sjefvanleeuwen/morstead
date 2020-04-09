﻿using Microsoft.AspNetCore.Mvc;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IServiceController
    {
        IExecutionResult Execute([FromBody] IExecuteRequest executeRequest);
        void GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        IParseResult Parse([FromBody] IParseRequest parseRequest);
    }
}