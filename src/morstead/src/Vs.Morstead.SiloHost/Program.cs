using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Vs.Morstead.Grains;
using Vs.Morstead.Orleans.Configuration;

namespace Vs.Morstead.SiloHost
{
    public class Program
    {


        public static Task Main(string[] args)
        {


            var Configuration = new ConfigurationBuilder()
               .AddYamlFile("config.yaml")

               .Build();
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
                        .MorsteadGrainStorage(options => { options.Name = "account-store"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "pub-sub-store"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "ArchiveStorage"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "session-store"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "content-store"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "bpm-process-store"; }, Configuration)
                        .MorsteadGrainStorage(options => { options.Name = "dir-store"; }, Configuration);

                    //.AddMemoryGrainStorage(name: "dir-store");
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
