using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class Parameter : IParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
