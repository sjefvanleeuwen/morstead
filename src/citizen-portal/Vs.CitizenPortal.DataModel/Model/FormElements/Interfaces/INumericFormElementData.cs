namespace Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces
{
    public interface INumericFormElementData : IFormElementData
    {
        int? Decimals { get; set; }
        bool DecimalsOptional { get; set; }
    }
}