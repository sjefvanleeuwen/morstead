using Orleans;
using System.Globalization;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.User;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public interface IUserAccountPersistentGrain : IGrainWithStringKey
    {
        Task RegisterUser(UserAccountState userAccount);
        Task SetLocale(CultureInfo cultureInfo);
        Task<UserAccountState> GetUserAccountInfo();

    }
}
