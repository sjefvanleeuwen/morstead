using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using System;
using System.Configuration;

namespace Vs.Morstead.SiloHost
{
    public static class MorsteadStorageStrategy
    {
        public static ISiloBuilder MorsteadGrainStorage(this ISiloBuilder builder, Action<MorsteadStorageConfiguration> configure, IConfiguration configuration)
        {
            var config = new MorsteadStorageConfiguration();
            configure(config);

            builder.AddAzureTableGrainStorage(
                name: config.Name,
                configureOptions: options =>
                {
                    options.TableName = config.Name.ToLower().Replace("-", "");
                    //options..ContainerName = config.Name.ToLower();
                    // Use JSON for serializing the state in storage
                    //options.UseFullAssemblyNames = true;
                   // options.UseJson = true;
                    options.ConnectionString = configuration["MorsteadStorage:ConnectionString"];
                });
            return builder;
        }
    }
}
