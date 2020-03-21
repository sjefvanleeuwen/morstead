namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public interface INumericFormElementData : IFormElementData
    {
        int? Decimals { get; set; }
        bool DecimalsOptional { get; set; }
    }
}