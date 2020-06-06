using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Vs.Morstead.Grains;

namespace Vs.Morstead.SiloHost
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "morstead-dev";
                            options.ServiceId = "vs-rules";
                        })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                        // note! volatile storage for morstead development purposes
                        .UseInMemoryReminderService()
                        .AddMemoryGrainStorage(name: "account-store")
                        .AddMemoryGrainStorage(name: "pub-sub-store")
                        .AddMemoryGrainStorage(name: "ArchiveStorage")
                        .AddMemoryGrainStorage(name: "session-store")
                        .AddMemoryGrainStorage(name: "content-store");
                    //.AddMemoryGrainStorage(name: "profileStore");
                    /*
                    .AddAzureBlobGrainStorage(
                        name: "profileStore",
                        configureOptions: options =>
                        {
                            // Use JSON for serializing the state in storage
                            options.UseJson = true;

                            // Configure the storage connection key
                            options.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
                        });*/

                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
