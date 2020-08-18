﻿using Bunit;
using Vs.CitizenPortal.Site.Shared.Components;
using Xunit;

namespace Vs.CitizenPortal.Site.Tests.Shared.Components
{
    public class CalculationHeaderTests : BlazorTestBase
    {
        [Fact]
        public void CalculationHeaderEmpty()
        {
            var cut = RenderComponent<CalculationHeader>();
            Assert.Empty(cut.Nodes);
            Assert.Empty(cut.FindAll("div"));
        }

        [Fact]
        public void CalculationHeaderFilledTitle()
        {
            var cut = RenderComponent<CalculationHeader>(
                (nameof(CalculationHeader.Title), "Title"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Single(cut.FindAll("h1"));
            Assert.Empty(cut.FindAll("h2"));
            Assert.Empty(cut.FindAll("h3"));
            Assert.Empty(cut.FindAll("h3 div.mdc-chip__text"));
            Assert.Empty(cut.FindAll("h3 > div.calc_heading_question"));
        }

        [Fact]
        public void CalculationHeaderFilledSubtitle()
        {
            var cut = RenderComponent<CalculationHeader>(
                (nameof(CalculationHeader.SubTitle), "SubTitle"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Empty(cut.FindAll("h1"));
            Assert.Single(cut.FindAll("h2"));
            Assert.Empty(cut.FindAll("h3"));
            Assert.Empty(cut.FindAll("h3 div.mdc-chip__text"));
            Assert.Empty(cut.FindAll("h3 > div.calc_heading_question"));
        }

        [Fact]
        public void CalculationHeaderNumberZero()
        {
            var cut = RenderComponent<CalculationHeader>(
                (nameof(CalculationHeader.Number), 0));
            Assert.Empty(cut.Nodes);
            Assert.Empty(cut.FindAll("h1"));
            Assert.Empty(cut.FindAll("h2"));
            Assert.Empty(cut.FindAll("h3"));
            Assert.Empty(cut.FindAll("h3 div.mdc-chip__text"));
            Assert.Empty(cut.FindAll("h3 > div.calc_heading_question"));
        }

        [Fact]
        public void CalculationHeaderFilledSubject()
        {
            var cut = RenderComponent<CalculationHeader>(
                (nameof(CalculationHeader.Subject), "Subject"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Empty(cut.FindAll("h1"));
            Assert.Empty(cut.FindAll("h2"));
            Assert.Single(cut.FindAll("h3"));
            Assert.Empty(cut.FindAll("h3 div.mdc-chip__text"));
            Assert.Single(cut.FindAll("h3 > div.calc_heading_question"));
        }

        [Fact]
        public void CalculationHeaderFilledWithMarkup()
        {
            var cut = RenderComponent<CalculationHeader>(
                (nameof(CalculationHeader.Title), "T<i>itl</i>e"),
                (nameof(CalculationHeader.SubTitle), "S<u>ubTi</u>tle"),
                (nameof(CalculationHeader.Subject), "S<strong>ub</strong>ject"));
            Assert.Single(cut.FindAll("i"));
            Assert.Equal("itl", cut.FindAll("i")[0].InnerHtml);
            Assert.Single(cut.FindAll("u"));
            Assert.Equal("ubTi", cut.FindAll("u")[0].InnerHtml);
            Assert.Single(cut.FindAll("strong"));
            Assert.Equal("ub", cut.FindAll("strong")[0].InnerHtml);
        }
    }
}
