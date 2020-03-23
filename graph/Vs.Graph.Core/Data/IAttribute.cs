namespace Vs.Graph.Core.Data
{
    public interface IAttribute
    {
        IAttributeType Type
        {
            get;
            set;
        }

        string Name { get; set; }
    }
}
