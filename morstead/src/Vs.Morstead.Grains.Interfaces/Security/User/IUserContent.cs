using Orleans;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public interface IUserContent : IGrainWithStringKey
    {
        Task<string> GetContentId();
        Task<IUserAccountPersistentGrain> GetOwner();
        Task SetOwner(IUserAccountPersistentGrain account);
        Task ContentSaved(SubscriptionCallback callback);
    }
}
