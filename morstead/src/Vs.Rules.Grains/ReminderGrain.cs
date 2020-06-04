using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;
using Vs.Orleans.Primitives.Time.Interfaces;

namespace Vs.Orleans.Primitives.Time
{
    [StorageProvider(ProviderName = "ArchiveStorage")]
    public class ReminderGrain : Grain, IReminderGrain
    {
        IGrainReminder _reminder = null;

        /// <summary>
        /// A grain that uses reminders must implement the IRemindable.RecieveReminder method.
        /// </summary>
        /// <param name="reminderName">Name of this Reminder</param>
        /// <param name="status">Status of this Reminder tick</param>
        /// <returns>
        /// Completion promise which the grain will resolve when it has finished processing
        /// this Reminder tick.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            // Grain-ception!
            var emailSenderGrain = GrainFactory
             .GetGrain<IEmailSenderGrain>(Guid.Empty); await emailSenderGrain.SendEmail(
            "homer@anykey.com",
            new[]
            {
                "marge@anykey.com",
                "bart@anykey.com",
                "lisa@anykey.com",
                "maggie@anykey.com"
            },
            "Everything's ok!",
            "This alarm will sound every 1 minute, as long as everything is ok!"
           );
        }

        public async Task Start()
        {
            if (_reminder != null)
            {
                return;
            }
            _reminder = await RegisterOrUpdateReminder(
                this.GetPrimaryKeyString(),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromMinutes(1) // apparently the minimum
            );
        }

        public async Task Stop()
        {
            if (_reminder == null)
            {
                return;
            }
            await UnregisterReminder(_reminder);
            _reminder = null;
        }

        /*
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
*/
    }
}