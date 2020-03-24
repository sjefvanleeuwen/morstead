using System;
using System.Collections.Generic;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects;
using Xunit;

namespace Vs.Cms.Core.Tests.Objects
{
    public class CultureContentTests
    {
        [Fact]
        public void ShouldAddContentSingleValue()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Title, "TestTitleContent");
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            sut.AddContent("testKey", FormElementContentType.Tag, "TestTagContent");
            //old one should be available
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestTagContent", sut.GetContent("testKey", FormElementContentType.Tag));
        }

        [Fact]
        public void ShouldAddContentDictionaryValue()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Options, new Dictionary<string, string>() {
                { "opt1", "option1" },
                { "opt2", "option2" }
            });
            var options = sut.GetContent("testKey", FormElementContentType.Options) as Dictionary<string, string>;
            Assert.Equal(2, options.Count);
            Assert.Equal("option1", options["opt1"]);
            Assert.Equal("option2", options["opt2"]);
        }

        [Fact]
        public void ShouldAddContentMultipleValues()
        {
            var sut = new CultureContent();
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContent("testKey", FormElementContentType.Title));

            sut.AddContent("testKey", new Dictionary<FormElementContentType, object> {
                { FormElementContentType.Title, "TestTitleContent" },
                { FormElementContentType.Tag, "TestTagContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestTagContent", sut.GetContent("testKey", FormElementContentType.Tag));

            sut.AddContent("testKey", new Dictionary<FormElementContentType, object> {
                { FormElementContentType.Title, "TestTitleContent" },
                { FormElementContentType.Question, "TestSummaryContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestSummaryContent", sut.GetContent("testKey", FormElementContentType.Question));
            //Tag should not be available
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContent("testKey", FormElementContentType.Tag));
        }

        [Fact]
        public void ShouldGetContent()
        {
            var sut = new CultureContent();
            sut.AddContent("testKey", FormElementContentType.Title, "TestTitleContent");
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
        }
    }
}
