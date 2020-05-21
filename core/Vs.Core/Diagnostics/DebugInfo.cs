using System;

namespace Vs.Core.Diagnostics
{
    public class DebugInfo : IDebugInfo
    {
        public DebugInfo(LineInfo start, LineInfo end)
        {
            Start = start;
            End = end;
        }

        public DebugInfo() { }

        public LineInfo Start { get; }
        public LineInfo End { get; }

        public override string ToString()
        {
            return $"(Line: {Start.Line}, Col: {Start.Col}, Idx: {Start.Index}) - (Line: {End.Line}, Col: {End.Col}, Idx: {End.Index})";
        }
    }
}
