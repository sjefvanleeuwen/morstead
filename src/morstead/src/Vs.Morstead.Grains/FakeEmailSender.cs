using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains
{

    public class FakeEmailSender : IEmailSender
    {
        private readonly ILogger<FakeEmailSender> _logger;
        public FakeEmailSender(ILogger<FakeEmailSender> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Pretend this actually sends an email.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string from, string[] to, string subject, string body)
        {
            var emailBuilder = new StringBuilder();
            emailBuilder.Append("Sending new Email...");
            emailBuilder.AppendLine();
            emailBuilder.Append($"From: {from}");
            emailBuilder.Append($"To: {string.Join(", ", to)}");
            emailBuilder.Append($"Subject: {subject}");
            emailBuilder.Append($"Body: {Environment.NewLine}{body}");
            _logger.LogInformation(emailBuilder.ToString());
            return Task.CompletedTask;
        }
    }
}