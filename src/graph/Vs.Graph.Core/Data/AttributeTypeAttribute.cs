namespace Vs.Graph.Core.Data
{
    public class AttributeTypeAttribute : System.Attribute
    {
        public readonly string Name;

        public AttributeTypeAttribute(string name) : base()
        {
            Name = name;
        }
    }
}
