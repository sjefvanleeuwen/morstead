using System.Collections.Generic;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public interface IBpmnTask
    {
        List<ExecutionListener> ExecutionListeners { get; set; }
    }
}
