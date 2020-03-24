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
        public void ShouldAddContentMultipleValues()
        {
            var sut = new CultureContent();
            Assert.Throws<IndexOutOfRangeException>(() => sut.GetContent("testKey", FormElementContentType.Title));

            sut.AddContent("testKey", new Dictionary<FormElementContentType, string> {
                { FormElementContentType.Title, "TestTitleContent" },
                { FormElementContentType.Tag, "TestTagContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestTagContent", sut.GetContent("testKey", FormElementContentType.Tag));

            sut.AddContent("testKey", new Dictionary<FormElementContentType, string> {
                { FormElementContentType.Title, "TestTitleContent" },
                { FormElementContentType.Summary, "TestSummaryContent" }
            });
            Assert.Equal("TestTitleContent", sut.GetContent("testKey", FormElementContentType.Title));
            Assert.Equal("TestSummaryContent", sut.GetContent("testKey", FormElementContentType.Summary));
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
