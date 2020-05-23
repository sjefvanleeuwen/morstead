using Orleans;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Vs.Publications.Grains.Interfaces
{
    public interface IPublicationGrain : IGrainWithStringKey
    {
        Task Create(ContentType contentType, object Content);
    }
}
