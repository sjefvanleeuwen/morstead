using Orleans;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.PubSub;
using Vs.Morstead.Grains.Interfaces.Security.User;

namespace Vs.Morstead.Grains.Interfaces.User
{
    public interface IUserContent : IGrainWithStringKey
    {
        Task<string> GetContentId();
        Task<IUserAccountPersistentGrain> GetOwner();
        Task SetOwner(IUserAccountPersistentGrain account);
        Task ContentSaved(SubscriptionCallback callback);
    }
}
