using Orleans;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Rules.Grains.User
{
    public interface IUserContent : IGrainWithStringKey
    {
        Task<IUserAccountPersistentGrain> GetOwner();
        Task SetOwner(IUserAccountPersistentGrain account);
        Task ContentSaved(SubscriptionCallback callback);
    }
}
