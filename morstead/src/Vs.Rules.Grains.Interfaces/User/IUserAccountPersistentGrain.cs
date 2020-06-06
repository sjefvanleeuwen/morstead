using Orleans;
using System.Globalization;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.User;

namespace Vs.Rules.Grains.User
{
    public interface IUserAccountPersistentGrain : IGrainWithStringKey
    {
        Task RegisterUser(UserAccountState userAccount);
        Task SetLocale(CultureInfo cultureInfo);
    }
}
