namespace Vs.Graph.Core.Data
{
    public interface IEdgeSchemaScript : IScriptable<IEdgeSchema>
    {
        INodeSchema Parent { get; }
    }
}