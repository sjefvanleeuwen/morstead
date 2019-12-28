namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class DebugInfo
    {
        public DebugInfo(LineInfo start, LineInfo end)
        {
            Start = start;
            End = end;
        }

        public LineInfo Start { get; }
        public LineInfo End { get; }
    }
}
