namespace Vs.Graph.Core.Data
{
    public interface IScriptable<T>
    {
        string CreateScript(T @object);
    }
}