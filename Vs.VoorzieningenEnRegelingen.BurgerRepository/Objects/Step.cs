using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class Step : IStep
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNumber { get; set; }
        public IQuestion Question { get; set; }

        public bool CanProceed => throw new System.NotImplementedException();
    }
}
