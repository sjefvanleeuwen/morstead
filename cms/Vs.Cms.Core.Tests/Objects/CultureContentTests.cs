using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects;
using Vs.Core.Extensions;
using Xunit;

namespace Vs.Cms.Core.Tests.Objects
{
    public class CultureContentTests
    {
        [Fact]
        public void ShouldAddContentSingleValue()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Title.GetDescription(), "TestTitleContent");
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            sut.AddContent("testKey", FormElementContentType.Label.GetDescription(), "TestTagContent");
            //old one should be available
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestTagContent", sut.GetContent("testKey", FormElementContentType.Label));
        }

        [Fact]
        public void ShouldAddContentDictionaryValue()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Description.GetDescription(), new Dictionary<string, string>() {
                { "opt1", "option1" },
                { "opt2", "option2" }
            });
            var options = sut.GetContent("testKey", FormElementContentType.Description) as Dictionary<string, string>;
            Assert.Equal(2, options.Count);
            Assert.Equal("option1", options["opt1"]);
            Assert.Equal("option2", options["opt2"]);
        }

        [Fact]
        public void ShouldAddContentMultipleValues()
        {
            var sut = new CultureContent();
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContent("testKey", FormElementContentType.Title));

            sut.AddContent("testKey", new Dictionary<string, object> {
                { FormElementContentType.Title.GetDescription(), "TestTitleContent" },
                { FormElementContentType.Label.GetDescription(), "TestTagContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestTagContent", sut.GetContent("testKey", FormElementContentType.Label));

            sut.AddContent("testKey", new Dictionary<string, object> {
                { FormElementContentType.Title.GetDescription(), "TestTitleContent" },
                { FormElementContentType.Question.GetDescription(), "TestSummaryContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestSummaryContent", sut.GetContent("testKey", FormElementContentType.Question));
            //Tag should not be available
            Assert.Null(sut.GetContent("testKey", FormElementContentType.Label));
        }

        [Fact]
        public void ShouldGetCompleteContent()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Title.GetDescription(), "TestTitleContent");
            sut.AddContent("testKey", FormElementContentType.Description.GetDescription(), "TestDescriptionContent {{test}}");
            sut.AddContent("testKey_sub", FormElementContentType.Description.GetDescription(), "subsub");
            var content = sut.GetCompleteContent("testKey");
            Assert.Equal(3, content.Count());
            Assert.Equal("TestTitleContent", content.ElementAt(0));
            Assert.Equal("TestDescriptionContent {{test}}", content.ElementAt(1));
            Assert.Equal("subsub", content.ElementAt(2));
        }

        [Fact]
        public void ShouldGetContent()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Title.GetDescription(), "TestTitleContent");
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
        }
    }
}
