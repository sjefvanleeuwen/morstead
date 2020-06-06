using Orleans;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Rules.Grains.Interfaces.Content
{
    public interface IContentPersistentGrain : IGrainWithStringKey, ISubscribableGrain
    {
        Task Save(ContentType contentType, Encoding encoding, byte[] contents);
        Task Save(ContentType contentType, Encoding encoding, string contents);
        Task<ContentState> Load();
    }
}
