using System.Collections.Generic;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public interface IBpmnTask
    {
        List<ExecutionListener> ExecutionListeners { get; set; }
        string Id { get;  }
        string Name { get;  }
        string Incoming { get; }
        string Outgoing { get; }
    }
}
