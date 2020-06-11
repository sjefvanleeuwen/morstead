using Vs.Morstead.Grains.Interfaces.Bpm;

namespace Vs.Morstead.Grains.Bpm
{
    public class BpmProcessState
    {
        /// <summary>
        /// Gets or sets the status of the process
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BpmProcessExecutionTypes Status { get; set; }
        /// <summary>
        /// Gets or sets the BPMN process in its XML annotation.
        /// </summary>
        /// <value>
        /// The BPMN XMl annotation.
        /// </value>
        public string Bpmn { get; set; }
    }
}