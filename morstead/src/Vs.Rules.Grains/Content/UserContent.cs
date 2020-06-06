using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.Content;
using Vs.Rules.Grains.Interfaces.Primitives.PubSub;
using Vs.Rules.Grains.User;

namespace Vs.Rules.Grains.Content
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
    }
}
