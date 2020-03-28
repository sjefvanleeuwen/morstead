namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface INumericFormElementData : IFormElementSingleValueDate
    {
        int? Decimals { get; set; }
        bool DecimalsOptional { get; set; }
    }
}