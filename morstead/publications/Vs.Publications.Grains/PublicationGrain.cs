using Orleans;
using Orleans.Runtime;
using System;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vs.Publications.Grains.Interfaces;
using Vs.Publications.Grains.Interfaces.StateModel;
using EasyCompressor;

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

            LZ4Compressor comp = new LZ4Compressor("", K4os.Compression.LZ4.LZ4Level.L12_MAX);
            Publication.State.Content = comp.Compress(contents);
            Publication.State.ContentLength = contents.Length;
            Publication.State.CompressionType = CompressionType.LZ4;
            Publication.State.ContentCompressedLength = Publication.State.Content.Length;
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
