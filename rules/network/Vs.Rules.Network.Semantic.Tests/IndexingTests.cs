using Xunit;

namespace Vs.Rules.Network.Semantic.Tests
{
    public class IndexingTests
    {
        [Fact]
        public void WordsShouldNotContainSpaces()
        {
            foreach (var word in WordData.words)
            {
                Assert.False(word.Contains(' '));
            }
        }
    }
}
