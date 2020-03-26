using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects;
using Vs.Cms.Core.Objects.Interfaces;
using Xunit;

namespace Vs.Cms.Core.Tests.Objects
{
    public class CultureContentContainerTests
    {
        [Fact]
        public void ShouldAdd()
        {
            var sut = new CultureContentContainer();
            var moq = InitMoqCultureContent();
            sut.Add(new CultureInfo("nl-NL"), moq.Object);

            Assert.NotNull(sut.Content);
            Assert.NotEmpty(sut.Content);
            Assert.Single(sut.Content);
            Assert.Equal("nl-NL", sut.Content.First().Key.Name);
            Assert.Equal("TestValue", sut.Content.First().Value.GetContent(string.Empty, FormElementContentType.Title));
        }

        [Fact]
        public void ShouldAddList()
        {
            var sut = new CultureContentContainer();
            var moq = InitMoqCultureContent();
            var cultureContents = new Dictionary<CultureInfo, ICultureContent>();
            cultureContents.Add(new CultureInfo("nl-NL"), moq.Object);
            sut.AddRange(cultureContents);

            Assert.NotNull(sut.Content);
            Assert.NotEmpty(sut.Content);
            Assert.Single(sut.Content);
            Assert.Equal("nl-NL", sut.Content.First().Key.Name);
            Assert.Equal("TestValue", sut.Content.First().Value.GetContent(string.Empty, FormElementContentType.Title));
        }

        private Mock<ICultureContent> InitMoqCultureContent()
        {
            var moq = new Mock<ICultureContent>();
            moq.Setup(m => m.GetContent(It.IsAny<string>(), It.IsAny<FormElementContentType>())).Returns("TestValue");
            return moq;
        }
    }
}
