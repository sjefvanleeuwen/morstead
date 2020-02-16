using System;
using YamlDotNet.Core;

namespace Vs.Core.Diagnostics
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

        public static DebugInfo MapDebugInfo(Mark start, Mark end)
        {
            if (start is null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end is null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            return new DebugInfo(
                start: new LineInfo(line: start.Line, col: start.Column, index: start.Index),
                end: new LineInfo(line: end.Line, col: end.Column, index: end.Index)
            );
        }
    }
}
