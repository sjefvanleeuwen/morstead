using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Controllers
{
    public class StepController : IStepController
    {
        public IStep GetStep(string yaml, IEnumerable<IParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public IStep GetStep(Uri yaml, IEnumerable<IParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
