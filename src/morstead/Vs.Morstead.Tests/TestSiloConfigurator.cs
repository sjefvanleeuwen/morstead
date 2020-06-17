using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.TestingHost;
using Vs.Morstead.Orleans.Configuration;

namespace Vs.Morstead.Tests
{
    public class TestSiloConfigurator : ISiloConfigurator
    {

        public void Configure(ISiloBuilder hostBuilder)
        {
            var Configuration = new ConfigurationBuilder()
                .AddYamlFile("config.yaml")
                .Build();
            hostBuilder
                .UseInMemoryReminderService()
                    .UseInMemoryReminderService()
                    .MorsteadGrainStorage(options => { options.Name = "account-store"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "pub-sub-store"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "ArchiveStorage"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "session-store"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "content-store"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "bpm-process-store"; }, Configuration)
                    .MorsteadGrainStorage(options => { options.Name = "dir-store"; }, Configuration);
                    //.AddMemoryGrainStorage(name: "account-store")
                    //.AddMemoryGrainStorage(name: "pub-sub-store")
                    //.AddMemoryGrainStorage(name: "ArchiveStorage")
                    //.AddMemoryGrainStorage(name: "session-store")
                    //.AddMemoryGrainStorage(name: "content-store")
                    //.AddMemoryGrainStorage(name: "bpm-process-store")
                    //.AddMemoryGrainStorage(name: "dir-store");
            //.AddFaultInjectionMemoryStorage("SlowMemoryStore", options => options.NumStorageGrains = 10, faultyOptions => faultyOptions.Latency = TimeSpan.FromMilliseconds(15));
        }
    }
}
