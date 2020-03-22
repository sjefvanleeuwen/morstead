using Microsoft.AspNetCore.Components.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class BlazorTestBase
    {
        protected TestHost _host;

        public BlazorTestBase()
        {
            var serviceCollection = new ServiceCollection();
            Initializer.Initialize(serviceCollection);
            Initializer.Initialize(serviceCollection);
            serviceCollection.AddLogging();

            _host = new TestHost(serviceCollection);
        }
    }
}
