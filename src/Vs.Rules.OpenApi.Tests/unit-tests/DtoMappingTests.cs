using Mapster;
using Vs.Rules.Core;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.OpenApi.Tests.unit_tests
{
    public class DtoMappingTests
    {
        [Fact]
        public void ShouldCorrectlyMapClientParameters()
        {
            TypeAdapterConfig<Vs.Rules.Core.Model.IParameter, OpenApi.v1.Features.discipl.Dto.ClientParameter>.NewConfig().Compile();

            var parameters = new Vs.Rules.Core.ParametersCollection() {
                new Vs.Rules.Core.Model.ClientParameter("alleenstaande", "ja", Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy"),
                new Vs.Rules.Core.Model.ClientParameter("toetsingsinkomen_aanvrager", (double)19000,  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Double, "Dummy"),
                new Vs.Rules.Core.Model.ClientParameter("vermogen_aanvrager", (double)1000,  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Double, "Dummy"),
                new Vs.Rules.Core.Model.ClientParameter("woonland", "Nederland",  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.List, "Dummy")
            } as Vs.Rules.Core.Interfaces.IParametersCollection;

            var apiParameters = parameters.Adapt<OpenApi.v1.Features.discipl.Dto.ClientParametersCollection>();
            Assert.True(apiParameters.Count == 4);
            for (int i = 0; i < parameters.Count; i++) {
                Assert.True(apiParameters[i].Name == parameters[i].Name);
                // Type not used on client as they are infered on the server.
                //Assert.True((int)apiParameters[i].Type == (int)parameters[i].Type);
                Assert.True(apiParameters[i].Value == parameters[i].Value);
            }

            // test implicit mapping
            var clientParameters = apiParameters.Adapt<Vs.Rules.Core.ParametersCollection>();
            for (int i = 0; i < parameters.Count; i++)
            {
                Assert.True(apiParameters[i].Name == clientParameters[i].Name);
                // Type not used on client as they are infered on the server.
                //Assert.True((int)apiParameters[i].Type == (int)clientParameters[i].Type);
                Assert.True(apiParameters[i].Value == clientParameters[i].Value);
            }

        }

        [Fact]
        public void ShouldCorrectlyMapServerParameters()
        {
            TypeAdapterConfig<Vs.Rules.Core.Model.IParameter, OpenApi.v1.Features.discipl.Dto.ServerParameter>.NewConfig().Compile();

            // Get The Server Model.
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            var model = result.Model;
            var parameters = new Vs.Rules.Core.ParametersCollection() {
                new Vs.Rules.Core.Model.Parameter("alleenstaande", "ja", Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Boolean, ref model),
                new Vs.Rules.Core.Model.Parameter("toetsingsinkomen_aanvrager", (double)19000,  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Double, ref model),
                new Vs.Rules.Core.Model.Parameter("vermogen_aanvrager", (double)1000,  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.Double, ref model),
                new Vs.Rules.Core.Model.Parameter("woonland", "Nederland",  Vs.Rules.Core.TypeInference.InferenceResult.TypeEnum.List, ref model)
            } as Vs.Rules.Core.Interfaces.IParametersCollection;

            var apiParameters = parameters.Adapt<OpenApi.v1.Features.discipl.Dto.ServerParametersCollection>();
            Assert.True(apiParameters.Count == 4);
            for (int i = 0; i < parameters.Count; i++)
            {
                Assert.True(apiParameters[i].Name == parameters[i].Name);
                Assert.True((int)apiParameters[i].Type == (int)parameters[i].Type);
                Assert.True(apiParameters[i].Value == parameters[i].Value);
            }

            // test implicit mapping
            var serverParameters = apiParameters.Adapt<Vs.Rules.Core.ParametersCollection>();
            for (int i = 0; i < parameters.Count; i++)
            {
                Assert.True(apiParameters[i].Name == serverParameters[i].Name);
                Assert.True((int)apiParameters[i].Type == (int)serverParameters[i].Type);
                Assert.True(apiParameters[i].Value == serverParameters[i].Value);
            }
        }
    }
}
