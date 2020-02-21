namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces
{
    public interface IStep
    {
        string Name { get; set; }
        string Description { get; set; }
        int OrderNumber { get; set; }
        IQuestion Question { get; set; }
        
        bool CanProceed { get; }
    }
}
