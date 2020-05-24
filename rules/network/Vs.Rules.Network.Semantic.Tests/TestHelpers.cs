using Vs.Rules.Core;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.Network.Semantic.Tests
{
    public static class TestHelpers
    {
        public static Model GetDefaultTestModel()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            Assert.False(result.IsError);
            return result.Model;
        }
    }
}
