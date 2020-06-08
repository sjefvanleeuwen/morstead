using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces;

namespace Vs.Morstead.Grains
{
    public class EmailSenderGrain : Grain, IEmailSenderGrain
    {
        private readonly ILogger<FakeEmailSender> _logger;
        public EmailSenderGrain(ILogger<FakeEmailSender> logger)
        {
            _logger = logger;
            _logger.Log(LogLevel.Warning, "logger made");
        }

        public async Task SendEmail(string from, string[] to, string subject, string body)
        {
            _logger.Log(LogLevel.Warning, "email sent");
            var emailSender = new FakeEmailSender(_logger);
            await emailSender.SendEmailAsync(from, to, subject, body);
        }
    }
}
