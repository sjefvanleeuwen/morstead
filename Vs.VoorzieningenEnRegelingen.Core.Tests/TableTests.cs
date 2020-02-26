using System;
using System.Collections.Generic;
using System.Text;
using Vs.Core.Diagnostics;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
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

        [Fact]
        public void CanDetermineTableLookupValueFromQuestion()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTableTests.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "woonland");
                Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.List);
                // This list can be used to do a selection of a valid woonland
                Assert.True(((List<object>)args.Parameters[0].Value).Count > 0);
                // Provide an anwser by selecting an item: Finland from the list
                parameters.Add(new ClientParameter(args.Parameters[0].Name, ((List<object>)args.Parameters[0].Value)[1]));
            };
            var executionResult = new ExecutionResult(ref parameters);
            try
            {
                var workflow = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                // The server lookup needs to be evaluated again to get the resulting woonlandfactor.
                // In this case the client will not need to have to answer another question.
                // Maybe this can be put in core, in order to make the client logic simpler.
                var evaluateAgain = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            Assert.True(parameters[0].Name == "woonland");
            Assert.True((string)parameters[0].Value == "Finland");
            Assert.True(parameters[1].Name == "woonlandfactor");
            Assert.True((double)parameters[1].Value == 0.7161);
            Assert.True(parameters[2].Name == "recht");
            Assert.True((bool)parameters[2].Value == true);

            // Quick Hack to see if recht is false by selecting woonland: Anders
            parameters.Clear();
            parameters.Add(new ClientParameter("woonland","Anders"));
            var recalculate = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True(parameters[0].Name == "woonland");
            Assert.True((string)parameters[0].Value == "Anders");
            Assert.True(parameters[1].Name == "woonlandfactor");
            Assert.True((double)parameters[1].Value == 0);
            Assert.True(parameters[2].Name == "recht");
            Assert.True((bool)parameters[2].Value == false);
            Assert.NotNull(recalculate.Stacktrace.FindLast(p => p.IsStopExecution == true));
        }
    }
}
