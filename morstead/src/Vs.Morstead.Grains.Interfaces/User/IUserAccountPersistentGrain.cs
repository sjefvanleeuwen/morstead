using Orleans;
using System.Globalization;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.User
{
    public interface IUserAccountPersistentGrain : IGrainWithStringKey
    {
        Task RegisterUser(UserAccountState userAccount);
        Task SetLocale(CultureInfo cultureInfo);
    }
}
