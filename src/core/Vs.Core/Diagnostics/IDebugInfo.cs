namespace Vs.Core.Diagnostics
{
    public interface IDebugInfo
    {
        LineInfo End { get; }
        LineInfo Start { get; }
    }
}