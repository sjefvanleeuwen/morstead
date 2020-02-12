using Vs.Graph.Core.Data;
using Xunit;

namespace Vs.DataProvider.MsSqlGraph.Tests
{
    public class EdgeTests
    {
        [Fact]
        public void Edge_Create()
        {
            var target = new EdgeSchema("likes");
          //  var script = new MsSqlGraphEdgeSchemaSript();
           // Assert.True(script.CreateScript(target) == "CREATE TABLE likes AS EDGE");
          //  var yaml = target.Serialize();
        }
    }
}
