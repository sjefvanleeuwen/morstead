using Bunit;
using Bunit.Mocking.JSInterop;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class BlazorTestBase : ComponentTestFixture
    {
        public BlazorTestBase()
        {
            var jsMock = Services.AddMockJsRuntime();
        }
    }
}
