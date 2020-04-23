using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Core.Enums;
using Vs.Core.Extensions;
using Vs.Rules.Core.Interfaces;
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

        [Fact]
        public void ShouldInitialize()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var moqTemplateEngine = InitMoqTemplateEngine();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            sut.Initialize("test: test");
            moqContentHandler.Verify(x => x.SetDefaultCulture(It.IsAny<CultureInfo>()), Times.Once());
            moqContentHandler.Verify(x => x.TranslateParsedContentToContent(It.IsAny<CultureInfo>(), It.IsAny<IDictionary<string, object>>()), Times.Once());
        }

        [Fact]
        public void ShouldGetUnresolvedParameters()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var moqCultureContent = new Mock<ICultureContent>();
            var moqTemplateEngine = InitMoqTemplateEngine();
            moqCultureContent.Setup(m => m.GetCompleteContent(It.IsAny<string>())).Returns(new List<string> { "a", "b" });
            moqContentHandler.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);
            
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object, moqTemplateEngine.Object);
            sut.GetUnresolvedParameters(It.IsAny<string>(), It.IsAny<IParametersCollection>());
            moqContentHandler.Verify(x => x.GetDefaultContent(), Times.Once());
            moqCultureContent.Verify(x => x.GetCompleteContent(It.IsAny<string>()), Times.Once());
            moqTemplateEngine.Verify(x => x.GetExpressionNames("a"), Times.Once());
            moqTemplateEngine.Verify(x => x.GetExpressionNames("b"), Times.Once());
        }

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
