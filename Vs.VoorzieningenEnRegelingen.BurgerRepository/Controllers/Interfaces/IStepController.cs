using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Controllers.Interfaces
{
    public interface IStepController
    {
        IStep GetStep(string yaml, IEnumerable<IParameter> parameters);

        IStep GetStep(Uri yaml, IEnumerable<IParameter> parameters);
    }
}
