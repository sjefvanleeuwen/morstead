using Orleans;
using Orleans.Runtime;
using Vs.Publications.Grains.Interfaces;
using Vs.Publications.Grains.Interfaces.StateModel;

namespace Vs.Publications.Grains
{
    public class PublicationGrain : Grain, IPublicationGrain
    {
        private IPersistentState<PublicationState> Publication { get; set; }

        public PublicationGrain([PersistentState("publication-state", "publication-store")] IPersistentState<PublicationState> publication)
        {
            Publication = publication;
        }
    }
}
