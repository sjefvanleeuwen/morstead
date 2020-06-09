using System;

namespace Vs.Core.Diagnostics
{
    /// <summary>
    /// Debugging information for code/script locations.
    /// </summary>
    /// <seealso cref="Vs.Core.Diagnostics.IDebugInfo" />
    public class DebugInfo : IDebugInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugInfo"/> class.
        /// </summary>
        /// <param name="start">The start of script line information.</param>
        /// <param name="end">The end of script line information.</param>
        public DebugInfo(LineInfo start, LineInfo end)
        {
            Start = start;
            End = end;
        }

        public static DebugInfo Default { get { return new DebugInfo(new LineInfo(0, 0, 0), new LineInfo(0, 0, 0)); } }

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
