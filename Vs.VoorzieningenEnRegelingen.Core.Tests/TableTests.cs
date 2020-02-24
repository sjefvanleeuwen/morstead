using System;
using System.Collections.Generic;
using System.Text;
using Vs.Core.Diagnostics;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class TableTests
    {
        [Fact]
        public void CanInferTypeFromColumValues()
        {
            // not needed for this test but not nullable.
            var debugInfoFake = new DebugInfo(new LineInfo(0, 0, 0), new LineInfo(0, 0, 0));
            var columnTypes = new List<ColumnType>();
            columnTypes.Add(new ColumnType(debugInfoFake, "woonland"));
            columnTypes.Add(new ColumnType(debugInfoFake, "factor"));
            var rows = new List<Row>();
            rows.Add(new Row(debugInfoFake, new List<Column>()
            {
                new Column(debugInfoFake,"Nederland"), new Column(debugInfoFake,"1.0")
            }));
            Table table = new Table(debugInfoFake, "woonlandfactoren",columnTypes, rows);
            Assert.True(columnTypes[0].Type == TypeInference.InferenceResult.TypeEnum.String);
            Assert.True(columnTypes[1].Type == TypeInference.InferenceResult.TypeEnum.Double);
        }
    }
}
