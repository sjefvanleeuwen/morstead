using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class EasyBooleanTests
    {
        private string _testYaml1 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Test A of B
   omschrijving: test
   keuze:
   - situatie: A
   - situatie: B
   recht: A
formules:
 - drempelinkomen:
     formule: 20941";

        private string _testYaml2 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Test A of B
   keuze:
   - situatie: A
   - situatie: B
   recht: A
formules:
 - drempelinkomen:
     formule: 20941";

        [Fact]
        public void ShouldAcceptKeuze()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml1);
            Assert.False(parseResult.IsError);
        }

        [Fact]
        public void ShouldStillReadDescription()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml1);
            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal("Test A of B", stepToTest.Name);
            Assert.Equal("test", stepToTest.Description);
        }

        [Fact]
        public void ShouldNotNeedDescription()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml2);

            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal("Test A of B", stepToTest.Name);
            Assert.Equal("Test A of B", stepToTest.Description);
        }

        [Fact]
        public void ShouldHaveTwoChoices()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml2);

            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal(2, stepToTest.Choices.Count());
            Assert.Equal("A", stepToTest.Choices.ElementAt(0));
            Assert.Equal("B", stepToTest.Choices.ElementAt(1));
        }

        //[Fact]
        //public void ShouldHaveTwoChoicesToDOFullTest()
        //{
        //    //List<ParametersCollection> parameters = new List<ParametersCollection>();
        //    var controller = new YamlScriptController();
        //    var parseResult = controller.Parse(YamlZorgtoeslag4.Body);

        //    //result should give 1 question with 2 parameters           
        //    Assert.True(true);
        //}
    }
}
