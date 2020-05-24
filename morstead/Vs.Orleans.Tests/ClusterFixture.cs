using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vs.Orleans.Tests
{
    public class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();

            //this.Cluster.
            Cluster = builder.Build();
            Cluster.Deploy();
        }

        public void Dispose()
        {
            Cluster.StopAllSilos();
        }

        public TestCluster Cluster { get; private set; }

        private class TestSiloConfigurator : ISiloConfigurator
        {

            public void Configure(ISiloBuilder hostBuilder)
            {
                hostBuilder
                    .UseInMemoryReminderService()
                    .AddMemoryGrainStorage(name: "session-store")
                    .AddMemoryGrainStorage(name: "publication-store");
                     
                //.AddFaultInjectionMemoryStorage("SlowMemoryStore", options => options.NumStorageGrains = 10, faultyOptions => faultyOptions.Latency = TimeSpan.FromMilliseconds(15));
            }
        }
    }

}
