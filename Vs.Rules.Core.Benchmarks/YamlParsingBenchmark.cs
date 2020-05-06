using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections.Generic;
using System.Linq;
using Vs.Rules.Core;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.TestData;

namespace Vs.VoorzieningenEnRegelingen.Benchmarks
{
    [SimpleJob(runStrategy: RunStrategy.Monitoring, targetCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class YamlParsingBenchmark
    {
        [Benchmark]
        public List<Formula> GetFunctionsSimpleYaml()
        {
            var parser = new YamlRuleParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Formulas().ToList();
        }

        [Benchmark]
        public List<Formula> GetFlowSimpleYaml()
        {
            var parser = new YamlRuleParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Formulas().ToList();
        }

        [Benchmark]
        public List<Table> GetTablesSimpleYaml()
        {
            var parser = new YamlRuleParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Tabellen().ToList();
        }

        [Benchmark]
        public StuurInformatie GetStuurInformatieYaml()
        {
            var parser = new YamlRuleParser(YamlTestFileLoader.Load(@"Simple.yaml"), null);
            return parser.Header();
        }
    }
}
