using Orleans;
using Orleans.Runtime;
using System;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Create(ContentType contentType, Encoding encoding, byte[] contents)
        {
            if (Publication.State.Content != null)
            {
                // should create only once, todo: implement version.
                throw new NotImplementedException();
            }
            Publication.State.Content = contents;
            Publication.State.ContentLength = contents.Length;
            Publication.State.ContentType = contentType;
            Publication.State.Encoding = encoding;
            await Publication.WriteStateAsync();
        }

        public async Task<PublicationState> Get()
        {
            return Publication.State;
        }
    }
}
