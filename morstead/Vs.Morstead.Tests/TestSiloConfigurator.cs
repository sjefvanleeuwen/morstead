using Orleans.Hosting;
using Orleans.TestingHost;

namespace Vs.Morstead.Tests
{
    public class TestSiloConfigurator : ISiloConfigurator
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
