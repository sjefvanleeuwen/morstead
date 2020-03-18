namespace Vs.Core.Diagnostics
{
    public interface ILineInfo
    {
        int Col { get; }
        int Index { get; }
        int Line { get; }
    }
}