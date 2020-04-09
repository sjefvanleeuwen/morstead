using System.Linq;
using Xunit;

namespace Vs.Cms.Core.Tests
{
    public class LiquidTests
    {
        [Fact]
        public void ShouldGetExpressionNames()
        {
            var sut = new Liquid();
            Assert.Equal("name", sut.GetExpressionNames("Hello {{name}}!").FirstOrDefault());
            var expressionnames = sut.GetExpressionNames("Hello {{world}}, I am {{Sam}} I am!");
            Assert.Equal("world", expressionnames.ElementAt(0));
            Assert.Equal("Sam", expressionnames.ElementAt(1));
        }
    }
}
