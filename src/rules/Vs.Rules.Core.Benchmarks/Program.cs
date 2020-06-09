using BenchmarkDotNet.Running;

namespace Vs.Rules.Core.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<YamlParsingBenchmark>();
        }
    }
}
