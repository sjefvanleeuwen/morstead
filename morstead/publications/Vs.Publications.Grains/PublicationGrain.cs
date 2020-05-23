using Orleans;
using Orleans.Runtime;
using System.Net.Mime;
using System.Threading.Tasks;
using Vs.Publications.Grains.Interfaces;
using Vs.Publications.Grains.Interfaces.StateModel;

namespace Vs.Publications.Grains
{
    public class PublicationGrain : Grain, IPublicationGrain, IGrainWithStringKey
    {
        private IPersistentState<PublicationState> Publication { get; set; }

        public PublicationGrain([PersistentState("publication-state", "publication-store")] IPersistentState<PublicationState> publication)
        {
            Publication = publication;
        }

        public async Task Create(ContentType contentType, object Content)
        {
        }
    }
}
