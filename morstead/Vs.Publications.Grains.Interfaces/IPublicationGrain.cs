using Orleans;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vs.Publications.Grains.Interfaces.StateModel;

namespace Vs.Publications.Grains.Interfaces
{
    public interface IPublicationGrain : IGrainWithStringKey
    {
        public Task Create(ContentType contentType, Encoding encoding, byte[] contents);
        public Task<PublicationState> Get();
    }
}
