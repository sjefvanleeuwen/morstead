using AngleSharp.Dom;
using Bunit;
using System;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components
{
    public class PaginationTests : BlazorTestBase
    {
        //[Fact]
        //public void PaginationEmpty()
        //{
        //    var cut = RenderComponent<Pagination>();
        //    Assert.Equal(3, cut.FindAll("li").Count);
        //    Assert.Equal(2, cut.FindAll("button").Count);
        //    Assert.True(cut.FindAll("button")[0].IsDisabled());
        //    Assert.True(cut.FindAll("button")[1].IsDisabled());
        //}

        //[Fact]
        //public void PaginationPrevious()
        //{
        //    var cut = RenderComponent<Pagination>(
        //        (nameof(Pagination.PreviousDisabled), false),
        //        (nameof(Pagination.PreviousText), "Previousousous"));
        //    Assert.Equal(2, cut.FindAll("button").Count);
        //    Assert.False(cut.FindAll("button")[0].IsDisabled());
        //    Assert.Contains("Previousousous", cut.FindAll("nav > ul > li")[0].InnerHtml);
        //    Assert.True(cut.FindAll("button")[1].IsDisabled());
        //}

        //[Fact]
        //public void PaginationNext()
        //{
        //    var cut = RenderComponent<Pagination>(
        //        (nameof(Pagination.NextDisabled), false),
        //        (nameof(Pagination.NextText), "Nextextext"));
        //    Assert.Equal(2, cut.FindAll("button").Count);
        //    Assert.True(cut.FindAll("button")[0].IsDisabled());
        //    Assert.False(cut.FindAll("button")[1].IsDisabled());
        //    Assert.Contains("Nextextext", cut.FindAll("nav > ul > li")[2].InnerHtml);
        //}

        //[Fact]
        //public void PaginationClickTests()
        //{
        //    var testValue = 0;
        //    Action previousStep = () => testValue--;
        //    Action nextStep = () => testValue++;

        //    var cut = RenderComponent<Pagination>(
        //        (nameof(Pagination.PreviousDisabled), false),
        //        (nameof(Pagination.Previous), previousStep),
        //        (nameof(Pagination.NextDisabled), false),
        //        (nameof(Pagination.Next), nextStep));

        //    var buttons = cut.FindAll("button");
        //    Assert.Equal(2, buttons.Count);
        //    var previous = buttons[0];
        //    var next = buttons[1];
        //    Assert.Equal(0, testValue);
        //    next.Click();
        //    Assert.Equal(1, testValue);
        //    next.Click();
        //    Assert.Equal(2, testValue);
        //    previous.Click();
        //    Assert.Equal(1, testValue);
        //    previous.Click();
        //    Assert.Equal(0, testValue);
        //    previous.Click();
        //    Assert.Equal(-1, testValue);
        //}
    }
}