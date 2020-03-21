namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class NumericFormElementData : FormElementData, INumericFormElementData
    {
        public int? Decimals { get; set; } = null;

        public bool DecimalsOptional { get; set; }
    }
}
