using Orleans.Hosting;
using Orleans.TestingHost;
using System;

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
                        .UseInMemoryReminderService()
                        .AddMemoryGrainStorage(name: "account-store")
                        .AddMemoryGrainStorage(name: "pub-sub-store")
                        .AddMemoryGrainStorage(name: "ArchiveStorage")
                        .AddMemoryGrainStorage(name: "session-store")
                        .AddMemoryGrainStorage(name: "content-store");
                //.AddFaultInjectionMemoryStorage("SlowMemoryStore", options => options.NumStorageGrains = 10, faultyOptions => faultyOptions.Latency = TimeSpan.FromMilliseconds(15));
            }
        }
    }

}
