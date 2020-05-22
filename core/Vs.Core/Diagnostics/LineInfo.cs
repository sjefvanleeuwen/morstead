namespace Vs.Core.Diagnostics
{
    /// <summary>
    /// Line info class which is a position indicator for code line, column and index.
    /// </summary>
    /// <seealso cref="Vs.Core.Diagnostics.ILineInfo" />
    public class LineInfo : ILineInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineInfo"/> class.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="col">The col.</param>
        /// <param name="index">The index.</param>
        public LineInfo(int line, int col, int index)
        {
            Line = line;
            Col = col;
            Index = index;
        }
        /// <summary>
        /// Line number of the position.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public int Line { get; }
        /// <summary>
        /// Column number of the position.
        /// </summary>
        /// <value>
        /// The col.
        /// </value>
        public int Col { get; }
        /// <summary>
        /// Index number of the position.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; }
    }
}
