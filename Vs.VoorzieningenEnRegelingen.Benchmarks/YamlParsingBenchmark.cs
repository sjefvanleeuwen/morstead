using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Benchmarks.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Benchmarks
{
    [SimpleJob(runStrategy:RunStrategy.Monitoring,targetCount:100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class YamlParsingBenchmark
    {
        [Benchmark]
        public List<Formula> GetFunctionsSimpleYaml()
        {
            var parser = new YamlParser(YamlSimple.Body, null);
            return parser.Formulas();
        }

        [Benchmark]
        public List<Formula> GetFlowSimpleYaml()
        {
            var parser = new YamlParser(YamlSimple.Body, null);
            return parser.Formulas();
        }

        [Benchmark]
        public List<Table> GetTablesSimpleYaml()
        {
            var parser = new YamlParser(YamlSimple.Body, null);
            return parser.Tabellen();
        }

        [Benchmark]
        public StuurInformatie GetStuurInformatieYaml()
        {
            var parser = new YamlParser(YamlSimple.Body, null);
            return parser.Header();
        }
    }
}
