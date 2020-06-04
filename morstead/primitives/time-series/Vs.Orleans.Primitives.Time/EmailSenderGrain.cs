using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Vs.Orleans.Primitives.Time.Interfaces;

namespace Vs.Orleans.Primitives.Time
{
    public class EmailSenderGrain : IEmailSenderGrain
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
