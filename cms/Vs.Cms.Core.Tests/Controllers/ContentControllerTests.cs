using Moq;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Core.Extensions;
using Xunit;

namespace Vs.Cms.Core.Tests.Controllers
{
    public class ContentControllerTests
    {
        [Fact]
        public void ShouldSetCulture()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var moqTemplateEngine = InitMoqTemplateEngine();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            moqContentHandler.Verify(x => x.SetDefaultCulture(It.IsAny<CultureInfo>()), Times.Never());
            sut.SetCulture(dutchCulture);
            moqContentHandler.Verify(x => x.SetDefaultCulture(dutchCulture), Times.Once());
        }

        [Fact]
        public void ShouldGetTextOptionOriginalValue()
        {
            var moqContentHandler = new Mock<IContentHandler>();
            var moqCultureContent = new Mock<ICultureContent>();
            var moqTemplateEngine = InitMoqTemplateEngine();
            moqContentHandler.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);

            var moqRenderStrategy = InitMoqRenderStrategy();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            Assert.Equal("opt", sut.GetText("semanticKey", FormElementContentType.Description, "opt"));
        }

        [Fact]
        public void ShouldGetTextByFormElementContentType()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var moqTemplateEngine = InitMoqTemplateEngine();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            var text = sut.GetText("semanticKey", FormElementContentType.Description);
            Assert.Equal("result1", text);
            text = sut.GetText("semanticKey", FormElementContentType.Label);
            Assert.Equal("result2", text);
        }

        [Fact]
        public void ShouldGetText()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var moqTemplateEngine = InitMoqTemplateEngine();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            var text = sut.GetText("semanticKey", FormElementContentType.Description.GetDescription());
            Assert.Equal("result1", text);
            text = sut.GetText("semanticKey", FormElementContentType.Label.GetDescription());
            Assert.Equal("result2", text);
        }

        //TODO MPS rewrite tests

        //[Fact]
        //public void ShouldSetParametersNoTexts()
        //{
        //    var moqRenderStrategy = InitMoqRenderStrategy();
        //    var moqContentHandler = InitMoqContentHandler();
        //    //make the contenthandler returns no texts
        //    var moqCultureContent = new Mock<ICultureContent>();
        //    moqCultureContent.Setup(m => m.GetCompleteContent(It.IsAny<string>())).Returns(new List<object>());
        //    moqContentHandler.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);
        //    var moqTemplateEngine = InitMoqTemplateEngine();
        //    var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
        //    sut.SetParameters("semanticKey", moqParameters.Object);
        //    moqTemplateEngine.Verify(x => x.GetExpressionNames(It.IsAny<string>()), Times.Never);
        //    moqParameters.Verify(x => x.GetAll(), Times.Once);
        //    moqYamlScriptController.Verify(x => x.EvaluateFormulaWithoutQA(ref It.Ref<IParametersCollection>.IsAny, It.IsAny<string>()), Times.Never);
        //}

        //[Fact]
        //public void ShouldSetParametersNoNeededParameters()
        //{
        //    var moqRenderStrategy = InitMoqRenderStrategy();
        //    var moqContentHandler = InitMoqContentHandler();
        //    //make the CultureContent returns already knows the needed variables so no call is done
        //    var moqCultureContent = InitCultureContent();
        //    var moqTemplateEngine = InitMoqTemplateEngine();
        //    //no applicabale texts
        //    var moqParameters = InitMoqParameters();
        //    moqParameters.Setup(m => m.GetAll()).Returns(new List<IParameter>
        //    {
        //        new ClientParameter("world", "Aarde", TypeInference.InferenceResult.TypeEnum.String, "semanticKey"),
        //        new ClientParameter("name", "Sam", TypeInference.InferenceResult.TypeEnum.String, "semanticKey"),
        //        new ClientParameter("Sam", "same", TypeInference.InferenceResult.TypeEnum.String, "semanticKey")
        //    });
        //    YamlScriptController(moqParameters.Object);
        //    var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
        //    sut.SetParameters("semanticKey", moqParameters.Object);
        //    moqTemplateEngine.Verify(x => x.GetExpressionNames(It.IsAny<string>()), Times.Exactly(3));
        //    moqParameters.Verify(x => x.GetAll(), Times.Once);
        //    moqYamlScriptController.Verify(x => x.EvaluateFormulaWithoutQA(ref It.Ref<IParametersCollection>.IsAny, It.IsAny<IEnumerable<string>>()), Times.Never);

        //}

        //[Fact]
        //public void ShouldSetParameters()
        //{
        //    var moqRenderStrategy = InitMoqRenderStrategy();
        //    var moqContentHandler = InitMoqContentHandler();
        //    //make the contenthandler returns no 
        //    var moqCultureContent = InitCultureContent();
        //    var moqTemplateEngine = InitMoqTemplateEngine();
        //    //no applicabale texts
        //    var moqParameters = InitMoqParameters();
        //    YamlScriptController(moqParameters.Object);
        //    var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
        //    sut.SetParameters("semanticKey", moqParameters.Object);
        //    moqTemplateEngine.Verify(x => x.GetExpressionNames(It.IsAny<string>()), Times.Exactly(3));
        //    moqParameters.Verify(x => x.GetAll(), Times.Exactly(2));
        //    moqYamlScriptController.Verify(x => x.EvaluateFormulaWithoutQA(ref It.Ref<IParametersCollection>.IsAny, It.IsAny<IEnumerable<string>>()), Times.Once);
        //}

        private Mock<IRenderStrategy> InitMoqRenderStrategy()
        {
            var moq = new Mock<IRenderStrategy>();
            moq.Setup(m => m.Render("template1", It.IsAny<object>())).Returns("result1");
            moq.Setup(m => m.Render("template2", It.IsAny<object>())).Returns("result2");
            moq.Setup(m => m.Render("option one", It.IsAny<object>())).Returns("option is ONE");
            return moq;
        }

        private Mock<IContentHandler> InitMoqContentHandler()
        {
            var moq = new Mock<IContentHandler>();
            var moqCultureContent = InitCultureContent();
            moq.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);
            return moq;
        }

        private Mock<ICultureContent> InitCultureContent()
        {
            var moq = new Mock<ICultureContent>();
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Description)).Returns("template1");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Label)).Returns("template2");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Description.GetDescription())).Returns("template1");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Label.GetDescription())).Returns("template2");
            moq.Setup(m => m.GetCompleteContent(It.IsAny<string>())).Returns(new List<object> {
                "Hello {{name}}!",
                "Dit is een text die geen variabele bevat",
                "Hello {{world}}, I am {{Sam}} I am!"
            });
            return moq;
        }

        private Mock<ITemplateEngine> InitMoqTemplateEngine()
        {
            var moq = new Mock<ITemplateEngine>();
            moq.Setup(m => m.GetExpressionNames("Hello {{name}}!")).Returns(new List<string> { "name" });
            moq.Setup(m => m.GetExpressionNames("Hello {{world}}, I am {{Sam}} I am!")).Returns(new List<string> { "world", "Sam" });
            return moq;
        }
    }
}
