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
        public void ShouldSetParsedContent()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqParsedContent = InitMoqParsedContent();
            var sut = new ContentController(moqRenderStrategy.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            moqParsedContent.Verify(x => x.SetDefaultCulture(dutchCulture), Times.Never());
            sut.SetParsedContent(moqParsedContent.Object);
            moqParsedContent.Verify(x => x.SetDefaultCulture(dutchCulture), Times.Once());
        }

        [Fact]
        public void ShouldSetCulture()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqParsedContent = InitMoqParsedContent();
            var sut = new ContentController(moqRenderStrategy.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetParsedContent(moqParsedContent.Object);
            moqParsedContent.Verify(x => x.SetDefaultCulture(It.IsAny<CultureInfo>()), Times.Never());
            sut.SetCulture(dutchCulture);
            moqParsedContent.Verify(x => x.SetDefaultCulture(dutchCulture), Times.Once());
        }

        [Fact]
        public void ShouldGetTextNullException()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var sut = new ContentController(moqRenderStrategy.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetCulture(dutchCulture);
            Assert.Throws<ArgumentNullException>(() => sut.GetText("semanticKey", FormElementContentType.Description));
        }

        [Fact]
        public void ShouldGetTextOptionException()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqParsedContent = InitMoqParsedContent();
            var sut = new ContentController(moqRenderStrategy.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetParsedContent(moqParsedContent.Object);
            sut.SetCulture(dutchCulture);
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetText("semanticKey", FormElementContentType.Options, "opt"));
        }

        [Fact]
        public void ShouldGetText()
        {
            var moqRenderStrategy = InitMoqRenderStrategy();
            var moqParsedContent = InitMoqParsedContent();
            var sut = new ContentController(moqRenderStrategy.Object);
            var dutchCulture = new CultureInfo("nl-NL");
            sut.SetParsedContent(moqParsedContent.Object);
            sut.SetCulture(dutchCulture);
            var text = sut.GetText("semanticKey", FormElementContentType.Description);
            Assert.Equal("result1", text);
            text = sut.GetText("semanticKey", FormElementContentType.Tag);
            Assert.Equal("result2", text);
            text = sut.GetText("semanticKey", FormElementContentType.Options, "opt1");
            Assert.Equal("option is ONE", text);
            text = sut.GetText("semanticKey", FormElementContentType.Options, "opt2");
            Assert.Equal("option is NOT ONE", text);
        }

        private Mock<IRenderStrategy> InitMoqRenderStrategy()
        {
            var moq = new Mock<IRenderStrategy>();
            moq.Setup(m => m.Render("template1", null)).Returns("result1");
            moq.Setup(m => m.Render("template2", null)).Returns("result2");
            moq.Setup(m => m.Render("option one", null)).Returns("option is ONE");
            moq.Setup(m => m.Render("option two", null)).Returns("option is NOT ONE");
            return moq;
        }

        private Mock<IParsedContent> InitMoqParsedContent()
        {
            var moq = new Mock<IParsedContent>();
            var moqCultureContent = InitCultureContent();
            moq.Setup(m => m.GetDefaultContent()).Returns(moqCultureContent.Object);
            return moq;
        }

        private Mock<ICultureContent> InitCultureContent()
        {
            var moq = new Mock<ICultureContent>();
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Description)).Returns("template1");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Tag)).Returns("template2");
            moq.Setup(m => m.GetContent("semanticKey", FormElementContentType.Options)).Returns(new Dictionary<string, string> { 
                { "opt1", "option one" }, 
                { "opt2", "option two" } 
            });
            return moq;
        }
    }
}
