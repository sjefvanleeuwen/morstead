using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces
{
    public interface IStepSequence
    {
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<IStep> Steps { get; set; }
        int CurrentStep { get; set; }
        IEnumerable<IParameter> Parameters { get; set; }
        IResult Result { get; set; }
        
        bool HasResult { get; }
        IStep GetNextStep();
        IStep GetPreviousStep();
        IStep GetFirstStep();
        IStep GetLastStep();
    }
}
