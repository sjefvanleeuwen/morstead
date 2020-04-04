using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.Core.Interface;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class TimePeriodTests
    {
        [Fact]
        public void TimePeriod_Can_Serialize()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            var range = new Itenso.TimePeriod.TimeRange();
            // range.m
            // "01/01/0001 00:00:00 - 31/12/9999 23:59:59 | 3652058.23:59"
            Assert.Equal("1-1-0001 00:00:00 - 31-12-9999 23:59:59 | 3652058.23:59", range.ToString());
            var s = range.ToString();
            var dateTimeStrings = s.Split('|')[0].Split(" - ");
            var l = new TimeRange(DateTime.Parse(dateTimeStrings[0], new CultureInfo("nl-NL")), DateTime.Parse(dateTimeStrings[1], new CultureInfo("nl-NL")));
            Assert.Equal(new DateTime(1, 1, 1), l.Start);
            Assert.Equal(new DateTime(9999, 12, 31, 23, 59, 59), l.End);
        }

        [Fact]
        public void NegativeTimePeriodWillBeFlipped()
        {
            var range = new TimeRange(new DateTime(2020, 3, 30), new DateTime(2020, 3, 29));
            Assert.Equal(new DateTime(2020, 3, 29), range.Start);
            Assert.Equal(new DateTime(2020, 3, 30), range.End);
        }

        [Fact]
        public void TimePeriodCanReadDutchDate()
        {
            var date = DateTime.Parse("11 maart 2020", new CultureInfo("nl-NL"));
            Assert.True(date.Day == 11);
            Assert.True(date.Month == 3);
            Assert.True(date.Year == 2020);
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void TimePeriodsCanBeReadFromTable()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlWettelijkeRente.Body);
            IParametersCollection parameters = new ParametersCollection();
            var executionResult = null as IExecutionResult;
            bool isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                switch (args.Parameters[0].Name)
                {
                    case "hoofdsom":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Double);
                        parameters.Add(new ClientParameter("hoofdsom", 7500));
                        break;
                    case "looptijd":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Period);
                        parameters.Add(new ClientParameter("vanaf", "30 Januari 2017"));
                        break;
                    case "soort_rente":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.List);
                        parameters.Add(new ClientParameter("soort_rente", 1500));
                        break; ;
                }
            };
            Assert.False(result.IsError);
            Assert.True(result.Model.Tables.Count == 2);
            Assert.True(result.Model.Tables[0].ColumnTypes[0].Type == TypeInference.InferenceResult.TypeEnum.DateTime);
            // Create TimeRange Array
            var range = new List<TimeRange>();
            for (int i = result.Model.Tables[0].Rows.Count - 1; i > 0; i--)
            {
                range.Add(new TimeRange(
                    DateTime.Parse(result.Model.Tables[0].Rows[i].Columns[0].Value.ToString(), new CultureInfo("nl-NL")),
                    DateTime.Parse(result.Model.Tables[0].Rows[i - 1].Columns[0].Value.ToString(), new CultureInfo("nl-NL")).AddDays(-1)));
            }
            range.Add(new TimeRange(DateTime.Parse(result.Model.Tables[0].Rows[0].Columns[0].Value.ToString(), new CultureInfo("nl-NL")), DateTime.MaxValue));
            /*
            try
            {
                executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            */
        }
    }
}
