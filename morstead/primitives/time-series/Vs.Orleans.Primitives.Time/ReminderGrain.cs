using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Vs.Orleans.Primitives.Time
{
    public class ReminderGrain : Grain, IRemindable
    {
        /// <summary>
        /// A grain that uses reminders must implement the IRemindable.RecieveReminder method.
        /// </summary>
        /// <param name="reminderName">Name of this Reminder</param>
        /// <param name="status">Status of this Reminder tick</param>
        /// <returns>
        /// Completion promise which the grain will resolve when it has finished processing
        /// this Reminder tick.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task ReceiveReminder(string reminderName, TickStatus status)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// To start a reminder, use the Grain.RegisterOrUpdateReminder method, which returns an
        /// IGrainReminder object
        /// </summary>
        /// <param name="reminderName">Name of the reminder.</param>
        /// <param name="dueTime">The due time.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        protected Task<IGrainReminder> RegisterOrUpdateReminder(string reminderName, TimeSpan dueTime, TimeSpan period)
        {
            var entry = new ReminderEntry
            {
                GrainRef = grainRef,
                ReminderName = reminderName,
                StartAt = DateTime.UtcNow.Add(dueTime),
                Period = period,
            };
        }

        /// <summary>
        /// Since reminders survive the lifetime of any single activation, they must be explicitly cancelled 
        /// (as opposed to being disposed). You cancel a reminder by calling Grain.UnregisterReminder:
        /// </summary>
        /// <param name="reminder">Reminder to unregister.</param>
        /// <returns>
        /// Completion promise for this operation.
        /// </returns>
        protected Task UnregisterReminder(IGrainReminder reminder)
        {

        }

    }
}