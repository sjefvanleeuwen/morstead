using Orleans;
using Orleans.Runtime;
using System.Globalization;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.User;

namespace Vs.Rules.Grains.User
{
    public class UserAccountPersistentGrain : Grain, IUserAccountPersistentGrain
    {
        private IPersistentState<UserAccountState> _account;

        public UserAccountPersistentGrain(
            [PersistentState("user-account", "account-store")]
            IPersistentState<UserAccountState> parameters)
        {
            _account = parameters;
        }

        public async Task RegisterUser(UserAccountState userAccount)
        {
            if (_account.State.Equals(null))
                throw new System.Exception("User Already Registered.");
            _account.State = userAccount;
            await _account.WriteStateAsync();
        }

        public async Task SetLocale(CultureInfo cultureInfo)
        {
            if (_account.State.Locale != cultureInfo)
                _account.State.Locale = cultureInfo;
            await _account.WriteStateAsync();
        }
    }
}
