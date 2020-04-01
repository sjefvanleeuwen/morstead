using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects.Interfaces;
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
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object);
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
            moqContentHandler.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);

            var moqRenderStrategy = InitMoqRenderStrategy();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            Assert.Equal("opt", sut.GetText("semanticKey", FormElementContentType.Option, null, "opt"));
        }

        [Fact]
        public void ShouldGetText()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqContentHandler = InitMoqContentHandler();
            var sut = new ContentController(moqRenderStrategy.Object, moqContentHandler.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            var text = sut.GetText("semanticKey", FormElementContentType.Description);
            Assert.Equal("result1", text);
            text = sut.GetText("semanticKey", FormElementContentType.Tag);
            Assert.Equal("result2", text);
            text = sut.GetText("semanticKey", FormElementContentType.Option);
            Assert.Equal("option is ONE", text);
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
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Tag)).Returns("template2");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Option)).Returns("option one");
            return moq;
        }
    }
}
