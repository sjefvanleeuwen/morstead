using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.PubSub;
using Vs.Morstead.Grains.Interfaces.Security.User;
using Vs.Rules.Grains.Interfaces.Content;

namespace Vs.Morstead.Grains.Content
{
    public class UserContent : Grain, IUserContent
    {
        public IPersistentState<UserContentState> Content { get; }
        public UserContent([PersistentState("user-content", "content-store")] IPersistentState<UserContentState> content)
        {
            Content = content;
        }

        public Task SetOwner(IUserAccountPersistentGrain userAccount)
        {
            Content.State.Owner = userAccount.GetPrimaryKeyString();
            return Task.CompletedTask;
        }

        public Task<IUserAccountPersistentGrain> GetOwner()
        {
            return Task.FromResult(GrainFactory.GetGrain<IUserAccountPersistentGrain>(Content.State.Owner));
        }

        public Task ContentSaved(SubscriptionCallback callback)
        {
            Content.State.ContentId = callback.GrainId;
            Content.WriteStateAsync();
            return Task.CompletedTask;
        }

        public async Task<string> GetContentId()
        {
            return Content.State.ContentId;
        }
    }
}
