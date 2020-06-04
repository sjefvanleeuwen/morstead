using Orleans;
using System.Threading.Tasks;

namespace Vs.Orleans.Primitives.Time.Interfaces
{
    public interface IEmailSenderGrain : IGrainWithGuidKey
    {
        Task SendEmail(string from, string[] to, string topic, string content);
    }
}
