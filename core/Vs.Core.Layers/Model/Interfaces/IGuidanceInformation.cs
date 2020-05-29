using Semver;
using Vs.Core.Diagnostics;

namespace Vs.Core.Layers.Model.Interfaces
{
    public interface IGuidanceInformation
    {
        string Domain { get; set; }
        LineInfo End { get; }
        string Organisation { get; set; }
        LineInfo Start { get; }
        string Status { get; set; }
        string Subject { get; set; }
        string Type { get; set; }
        SemVersion Version { get; set; }
        int Year { get; set; }
    }
}