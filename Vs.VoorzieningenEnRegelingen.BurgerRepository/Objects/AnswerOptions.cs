using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class AnswerOptions : IAnswerOptions
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
