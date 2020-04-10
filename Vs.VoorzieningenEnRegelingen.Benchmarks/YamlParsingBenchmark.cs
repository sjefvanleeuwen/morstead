using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;

namespace Vs.VoorzieningenEnRegelingen.Benchmarks
{
    [SimpleJob(runStrategy: RunStrategy.Monitoring, targetCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class YamlParsingBenchmark
    {
        [Benchmark]
        public List<Formula> GetFunctionsSimpleYaml()
        {
            var parser = new YamlParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Formulas().ToList();
        }

        [Benchmark]
        public List<Formula> GetFlowSimpleYaml()
        {
            var parser = new YamlParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Formulas().ToList();
        }

        [Benchmark]
        public List<Table> GetTablesSimpleYaml()
        {
            var parser = new YamlParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Tabellen().ToList();
        }

        [Benchmark]
        public StuurInformatie GetStuurInformatieYaml()
        {
            var parser = new YamlParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Header();
        }
    }
}
