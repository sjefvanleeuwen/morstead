namespace Vs.Graph.Core.Data
{
    public interface INodeSchema: IIdentifiable, ISerialize
    {
        Attributes Attributes { get; }
        //Guid ObjectId { get; }
        Edges Edges { get; }
    }
}