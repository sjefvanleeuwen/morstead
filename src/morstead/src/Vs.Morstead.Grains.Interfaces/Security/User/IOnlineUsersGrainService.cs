using Orleans.Services;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    /// <summary>
    /// Grain Service for monitoring online users. It runs in the silo from starup to
    /// shutdown.
    /// </summary>
    /// <seealso cref="Orleans.Services.IGrainService" />
    public interface IOnlineUsersGrainService : IGrainService
    {
        /// <summary>
        /// Ping this Task periodically to let others know the user is alive and kicking.
        /// </summary>
        /// <param name="UserAccountGrainId">The user account grain identifier.</param>
        /// <returns></returns>
        Task SetAlive(string userAccountGrainId);
    }
}
