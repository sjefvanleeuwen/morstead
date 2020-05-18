using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vs.Rules.OrleansTests
{
    public class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();

            //this.Cluster.
            this.Cluster = builder.Build();
            this.Cluster.Deploy();
        }

        public void Dispose()
        {
            this.Cluster.StopAllSilos();
        }

        public TestCluster Cluster { get; private set; }

        private class TestSiloConfigurator : ISiloConfigurator
        {

            public void Configure(ISiloBuilder hostBuilder)
            {
                hostBuilder
                    .AddMemoryGrainStorage(name: "session-store");
                //.AddFaultInjectionMemoryStorage("SlowMemoryStore", options => options.NumStorageGrains = 10, faultyOptions => faultyOptions.Latency = TimeSpan.FromMilliseconds(15));
            }
        }
    }

}
