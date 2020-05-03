namespace Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces
{
    public interface INumericFormElementData : IFormElementData
    {
        int? Decimals { get; set; }
        bool DecimalsOptional { get; set; }
    }
}