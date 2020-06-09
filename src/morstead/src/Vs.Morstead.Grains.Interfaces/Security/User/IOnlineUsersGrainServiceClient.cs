using Orleans.Services;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public interface IOnlineUsersGrainServiceClient : 
        IGrainServiceClient<IOnlineUsersGrainService>,
        IOnlineUsersGrainService
    {
    }
}
