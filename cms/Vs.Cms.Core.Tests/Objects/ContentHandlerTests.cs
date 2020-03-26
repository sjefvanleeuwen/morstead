using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Helper;
using Vs.Cms.Core.Objects;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Cms.Core.Tests.TestYaml;
using Xunit;

namespace Vs.Cms.Core.Tests.Objects
{
    public class ContentHandlerTests
    {
        [Fact]
        public void GetDefaultCultureEmpty()
        {
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetDefaultCulture());
        }

        [Fact]
        public void GetDefaultCulture()
        {
            //return the only one available
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null }
            });
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);

            //return the first available
            sut = new ContentHandler(container);
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("de-DE"), null }
            });
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);

            //set the cultureContents again, the defaultCulture is reset
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Equal("en-GB", sut.GetDefaultCulture().Name);

            //return one that has a higher priority
            sut = new ContentHandler(container);
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Equal("en-GB", sut.GetDefaultCulture().Name);

            //return the one set
            sut = new ContentHandler(container);
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            sut.SetDefaultCulture(new CultureInfo("ru-KZ"));
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);
        }

        [Fact]
        public void SetDefaultCulture()
        {
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            //set a couple of cultures
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), null },
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });

            //make sure it is something else before setting
            var cultureInfo = sut.GetDefaultCulture();
            Assert.Equal("nl", cultureInfo.TwoLetterISOLanguageName);
            Assert.Equal("Dutch (Netherlands)", cultureInfo.DisplayName);
            Assert.Equal("nl-NL", cultureInfo.Name);

            sut.SetDefaultCulture(new CultureInfo("ru-KZ"));
            cultureInfo = sut.GetDefaultCulture();
            Assert.Equal("ru", cultureInfo.TwoLetterISOLanguageName);
            Assert.Equal("русский (Казахстан)", cultureInfo.DisplayName);
            Assert.Equal("ru-KZ", cultureInfo.Name);
        }

        [Fact]
        public void ShouldGetContentByCulture()
        {
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            //set a couple of cultures
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), null },
                { new CultureInfo("ru-KZ"), new CultureContent() },
                { new CultureInfo("en-GB"), null }
            });
            sut.SetDefaultCulture(new CultureInfo("ru-Kz"));
            Assert.NotNull(sut.GetContentByCulture(new CultureInfo("ru-Kz")));
        }

        [Fact]
        public void ShouldGetContentByCultureUnknownCulture()
        {
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            //set a couple of cultures
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContentByCulture(new CultureInfo("ru-KZ")));
        }

        [Fact]
        public void ShouldGetDefaultContent()
        {
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            //set a couple of cultures
            sut.AddCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), new CultureContent() },
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.NotNull(sut.GetDefaultContent());
        }

        [Fact]
        public void ShouldTransLateParsedContentToContent()
        {
            //parse the content tested in the YamlContentParser, do not mock in this case
            var container = new CultureContentContainer();
            var sut = new ContentHandler(container);
            var dutch = new CultureInfo("nl-NL");
            sut.TransLateParsedContentToContent(dutch, YamlContentParser.RenderContentYamlToObject(ContentYamlTest1.Body));
            var cultureContent = sut.GetContentByCulture(dutch);
            Assert.Equal("Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.",
                cultureContent.GetContent("stap.woonsituatie", FormElementContentType.Description));
        }
    }
}
