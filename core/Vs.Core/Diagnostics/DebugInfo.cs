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
        /// <summary>
        /// Gets the starting point of the item to debug.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public LineInfo Start { get; }
        /// <summary>
        /// Gets the endpoint of the item to debug.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public LineInfo End { get; }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"(Line: {Start.Line}, Col: {Start.Col}, Idx: {Start.Index}) - (Line: {End.Line}, Col: {End.Col}, Idx: {End.Index})";
        }
    }
}
