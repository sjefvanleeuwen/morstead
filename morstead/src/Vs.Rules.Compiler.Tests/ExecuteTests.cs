using Belastingdienst.Zorg.Toeslagen.Zorgtoeslag;
using System.Collections.Generic;
using System.Diagnostics;
using Vs.Rules.Grains.Interfaces;
using Xunit;

namespace Vs.Rules.Compiler.Tests
{
    public class ExecuteTests
    {
        [Fact]
        public async void PerformanceTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var answers = new Stack<Answer>();
                answers.Push(new Answer() { Name = "a", Value = 20000.0 });
                answers.Push(new Answer() { Name = "b", Value = false });
                answers.Push(new Answer() { Name = "c", Value = false });
                answers.Push(new Answer() { Name = "d", Value = true });
            }
            stopWatch.Stop();
            var ms = (double)stopWatch.ElapsedTicks / 10000;
        }
        [Fact]
        public async void PerformanceTest2()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var answers = new List<Answer>();
                answers.Add(new Answer() { Name = "a", Value = 20000.0 });
                answers.Add(new Answer() { Name = "b", Value = false });
                answers.Add(new Answer() { Name = "c", Value = false });
                answers.Add(new Answer() { Name = "d", Value = true });
            }
            stopWatch.Stop();
            var ms = (double)stopWatch.ElapsedTicks / 10000;
        }

                [Fact]
        public async void PerformanceTest3()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var answers = new List<Answer>(4);
                answers.Add(new Answer() { Name = "a", Value = 20000.0 });
                answers.Add(new Answer() { Name = "b", Value = false });
                answers.Add(new Answer() { Name = "c", Value = false });
                answers.Add(new Answer() { Name = "d", Value = true });
            }
            stopWatch.Stop();
            var ms = (double)stopWatch.ElapsedTicks / 10000;
        }

        [Fact]
        public async void PerformanceTest4()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var answers = new Answer[4];
                answers[0] = new Answer() { Name = "a", Value = 20000.0 };
                answers[1] = (new Answer() { Name = "b", Value = false });
                answers[2] = (new Answer() { Name = "c", Value = false });
                answers[3] =(new Answer() { Name = "d", Value = true });
            }
            stopWatch.Stop();
            var ms = (double)stopWatch.ElapsedTicks / 10000;
        }

        [Fact]
        public async void ShouldExecuteWithAnswers()
        {
            Stopwatch stopWatch = new Stopwatch();
          //  stopWatch.Start();
            var target = new SimpleRuleGrain();
            //  stopWatch.Stop();
            //
            //     var result = target.Execute(null);
            //   Assert.True(result.Count == 1);
            //    Assert.Contains(result, p =>p.Name == "a");

            var answers = new Answer[5];
            answers[0] = new Answer()  { Name = "a", Value = 20000.0 };
            answers[1] = (new Answer() { Name = "b", Value = false });
            answers[2] = (new Answer() { Name = "c", Value = false });
            answers[3] = (new Answer() { Name = "d", Value = true });
            answers[4] = (new Answer() { Name = "woonland", Value = "Nederland" });
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var result = await target.Execute(answers);
            }
            stopWatch.Stop();
            var ms = (double)stopWatch.ElapsedTicks / 10000;
            //target.Execute(new Stack<Answer>() { new Answer() { Name = "a", Value = 20000 } }) ;
        }
    }
}
