using Orleans;
using System.Text;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Morstead.Grains.Interfaces.Content
{
    public interface IContentPersistentGrain : IGrainWithStringKey, ISubscribableGrain
    {
        Task Save(string contentType, EncodingType encoding, byte[] contents);
        Task Save(string contentType, EncodingType encoding, string contents);
        Task<ContentState> Load();
    }
}
