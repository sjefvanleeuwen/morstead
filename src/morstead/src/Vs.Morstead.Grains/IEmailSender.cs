using System.Threading.Tasks;

namespace Vs.Morstead.Grains
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string[] to, string subject, string body);
    }
}