using System.Collections.Generic;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public interface ITarget
    {
        string Id { get; }
        string Name { get; }
        List<string> Incoming { get; set; }
        List<string> Outgoing { get; set; }
    }
}