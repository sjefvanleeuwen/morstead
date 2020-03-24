using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Objects;
using Vs.Cms.Core.Objects.Interfaces;
using Xunit;

namespace Vs.Cms.Core.Tests.Objects
{
    public class ParsedContentTests
    {
        [Fact]
        public void GetDefaultCultureEmpty()
        {
            var sut = new ParsedContent();
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetDefaultCulture());
        }

        [Fact]
        public void GetDefaultCulture()
        {
            //return the only one available
            var sut = new ParsedContent();
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null }
            });
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);

            //return the first available
            sut = new ParsedContent();
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("de-DE"), null }
            });
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);

            //set the cultureContents again, the defaultCulture is reset
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Equal("en-GB", sut.GetDefaultCulture().Name);

            //return one that has a higher priority
            sut = new ParsedContent();
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Equal("en-GB", sut.GetDefaultCulture().Name);

            //return the one set
            sut = new ParsedContent();
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            sut.SetDefaultCulture(new CultureInfo("ru-KZ"));
            Assert.Equal("ru-KZ", sut.GetDefaultCulture().Name);
        }

        [Fact]
        public void SetDefaultCulture()
        {
            var sut = new ParsedContent();
            //set a couple of cultures
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
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

            //underscore is same as dash
            sut.SetDefaultCulture(new CultureInfo("en_GB"));
            cultureInfo = sut.GetDefaultCulture();
            Assert.Equal("en", cultureInfo.TwoLetterISOLanguageName);
            Assert.Equal("English (United Kingdom)", cultureInfo.DisplayName);
            Assert.Equal("en-GB", cultureInfo.Name);
        }

        [Fact]
        public void ShouldGetContentByCulture()
        {
            var sut = new ParsedContent();
            //set a couple of cultures
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
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
            var sut = new ParsedContent();
            //set a couple of cultures
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContentByCulture(new CultureInfo("ru-KZ")));
        }

        [Fact]
        public void ShouldGetDefaultContent()
        {
            var sut = new ParsedContent();
            //set a couple of cultures
            sut.SetCultureContents(new Dictionary<CultureInfo, ICultureContent> {
                { new CultureInfo("nl-NL"), new CultureContent() },
                { new CultureInfo("ru-KZ"), null },
                { new CultureInfo("en-GB"), null }
            });
            Assert.NotNull(sut.GetDefaultContent());
        }
    }
}
