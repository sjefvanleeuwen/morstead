using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class StepSequence : IStepSequence
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IStep> Steps { get; set; }
        public int CurrentStep { get; set; }
        public IEnumerable<IParameter> Parameters { get; set; }
        public IResult Result { get; set; }

        public bool HasResult => throw new System.NotImplementedException();

        public IStep GetFirstStep()
        {
            throw new System.NotImplementedException();
        }

        public IStep GetLastStep()
        {
            throw new System.NotImplementedException();
        }

        public IStep GetNextStep()
        {
            throw new System.NotImplementedException();
        }

        public IStep GetPreviousStep()
        {
            throw new System.NotImplementedException();
        }
    }
}
