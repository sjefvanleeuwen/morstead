using Microsoft.AspNetCore.Components.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class PaginationTests : BlazorTestBase
    {
        [Fact]
        public void PaginationEmpty()
        {
            var component = _host.AddComponent<Pagination>();
            Assert.False(component.Instance.PreviousDisabled);
            Assert.False(component.Instance.NextDisabled);
            Assert.Equal(2, component.Find("nav > ul").Elements().Count());
            Assert.Single(component.Find("nav > ul").Elements().First().Elements());
            Assert.Equal("a", component.Find("nav > ul").Elements().First().Elements().First().Name);
            Assert.Single(component.Find("nav > ul").Elements().Last().Elements());
            Assert.Equal("a", component.Find("nav > ul").Elements().Last().Elements().First().Name);
        }

        [Fact]
        public void PaginationDisbled()
        {
            var variables = new Dictionary<string, object> {
                { "PreviousDisabled", true },
                { "NextDisabled", true }
            };
            var component = _host.AddComponent<Pagination>(variables);
            Assert.True(component.Instance.PreviousDisabled);
            Assert.True(component.Instance.NextDisabled);
            Assert.Equal(2, component.Find("nav > ul").Elements().Count());
            Assert.Empty(component.Find("nav > ul").Elements()[0].Elements());
            Assert.Empty(component.Find("nav > ul").Elements()[1].Elements());
        }

        [Fact]
        public void PaginationTexts()
        {
            var variables = new Dictionary<string, object> {
                { "PreviousDisabled", false },
                { "NextDisabled", false },
                { "PreviousText", "Vorige test" },
                { "NextText", "Volgende test" },
                { "ScreenreaderDescription", "Beschrijving hulp" }
            };
            var component = _host.AddComponent<Pagination>(variables);
            var previousLink = component.Find("nav > ul").Elements()[0].Elements()[0];
            Assert.Equal("span", previousLink.Elements()[0].Name);
            Assert.Equal("span", previousLink.Elements()[1].Name);
            Assert.Equal("Vorige test", previousLink.Elements()[0].InnerHtml);
            Assert.Equal("Beschrijving hulp", previousLink.Elements()[1].InnerHtml);
            var nextLink = component.Find("nav > ul").Elements()[1].Elements()[0];
            Assert.Equal("span", nextLink.Elements()[0].Name);
            Assert.Equal("span", nextLink.Elements()[1].Name);
            Assert.Equal("Volgende test", nextLink.Elements()[0].InnerHtml);
            Assert.Equal("Beschrijving hulp", nextLink.Elements()[1].InnerHtml);
        }



        [Fact]
        public void PaginationClickTests()
        {
            var testValue = 0;
            Action previousStep = () => testValue--;
            Action nextStep = () => testValue++;

            var variables = new Dictionary<string, object> {
                { "Previous", previousStep },
                { "Next", nextStep }
            };
            var component = _host.AddComponent<Pagination>(variables);
            var buttons = component.FindAll("nav > ul > li > a").ToList();
            Assert.Equal(2, buttons.Count());
            var previous = buttons[0];
            var next = buttons[1];
            Assert.Equal(0, testValue);
            next.Click();
            Assert.Equal(1, testValue);
            next.Click();
            Assert.Equal(2, testValue);
            previous.Click();
            Assert.Equal(1, testValue);
            previous.Click();
            Assert.Equal(0, testValue);
            previous.Click();
            Assert.Equal(-1, testValue);
        }
    }
}