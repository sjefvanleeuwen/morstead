using Vs.Core.IntegrationTesting.OpenApi;
using Vs.Rules.OpenApi.v1.Features.discipl.Controllers;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.OpenApi.Tests.v1.Features.discipl
{
    public class FeatureTests : IClassFixture<TestFixture<Startup>>
    {
        private RulesClient Client;

        public FeatureTests(TestFixture<Startup> fixture)
        {
            Client = new RulesClient(fixture.Client);
        }

        [Fact]
        public async void CanExecuteRuleYamlFromContentsUT()
        {
            RulesControllerDiscipl controller = new RulesControllerDiscipl();
            var result = await controller.ExecuteRuleYamlContents(new OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlFromContentRequest()
            {
                Yaml = YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml")
            });
        }

        [Fact]
        public async void ShouldProvideDebugInformationUT()
        {
            RulesControllerDiscipl controller = new RulesControllerDiscipl();
            var result = await controller.ExecuteRuleYamlContents(new OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlFromContentRequest()
            {
                Yaml = YamlTestFileLoader.Load(@"Malformed/HeaderUnknownProperty.yaml")
            });
            var value = ((Vs.Rules.OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
            Assert.NotEmpty(value.ParseResult.FormattingExceptions);
            Assert.NotNull(value.ParseResult.FormattingExceptions[0].DebugInfo);
            Assert.NotNull(value.ParseResult.FormattingExceptions[0].Message);
        }

        [Fact]
        public async void CanExecuteRuleYamlFromContents()
        {
            /*
            var result = await Client.ExecuteRuleYamlContentsAsync(new ExecuteRuleYamlFromContentRequest()
            {
                Yaml = YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml")
            });
            */
        }
    }
}
