using Orleans;
using Orleans.Runtime;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Content;
using Vs.Morstead.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Morstead.Grains.Content
{
    public class ContentPersistentGrain : Grain, IContentPersistentGrain
    {
        private IPersistentState<ContentState> Content { get; set; }
        private IPubSubGrain pubSub { get; set; }

        public ContentPersistentGrain([PersistentState("content-state", "content-store")] IPersistentState<ContentState> content)
        {
            Content = content;
        }

        public override Task OnActivateAsync()
        {
            pubSub = GrainFactory.GetGrain<IPubSubGrain>("pubsub-" + this.GetGrainIdentity().PrimaryKeyString);
            pubSub.SetPublishingGrain(typeof(IContentPersistentGrain), this.GetGrainIdentity().PrimaryKeyString);
            return base.OnActivateAsync();
        }

        public async Task Save(ContentType contentType, Encoding encoding, string contents)
        {
            await Save(contentType, encoding, encoding.GetBytes(contents));
        }

        public async Task Save(ContentType contentType, Encoding encoding, byte[] contents)
        {
            var compression = GrainFactory.GetGrain<ICompressionWorkerGrain>(0);
            Content.State.Content = await compression.Compress(CompressionType.LZ4, contents);
            Content.State.CompressionType = CompressionType.LZ4;
            Content.State.ContentCompressedLength = Content.State.Content.Length;
            Content.State.ContentLength = contents.Length;
            Content.State.ContentType = contentType;
            Content.State.Encoding = encoding;
            await Content.WriteStateAsync();
            await pubSub.Notify("save");
        }

        public async Task<ContentState> Load()
        {
            var compression = GrainFactory.GetGrain<ICompressionWorkerGrain>(0);
            await Content.ReadStateAsync();
            Content.State.Content = await compression.Decompress(
                Content.State.CompressionType, Content.State.Content);
            return Content.State;
        }

        public Task<IPubSubGrain> GetPubSub()
        {
            return Task.FromResult(pubSub);
        }
    }
}
