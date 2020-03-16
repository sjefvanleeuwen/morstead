using Microsoft.AspNetCore.Components.Testing;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class BlazorTestBase
    {
        protected TestHost _host;

        public BlazorTestBase()
        {
            _host = new TestHost();
        }
    }
}
