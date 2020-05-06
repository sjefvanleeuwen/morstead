using BenchmarkDotNet.Running;

namespace Vs.VoorzieningenEnRegelingen.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<YamlParsingBenchmark>();
        }
    }
}
