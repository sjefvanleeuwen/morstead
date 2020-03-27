namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface INumericFormElementData : IFormElementSingleValue
    {
        int? Decimals { get; set; }
        bool DecimalsOptional { get; set; }
    }
}