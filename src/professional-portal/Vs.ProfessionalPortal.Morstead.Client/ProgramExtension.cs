using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Runtime.Messaging;
using System;
using System.Threading.Tasks;

namespace Vs.ProfessionalPortal.Morstead.Client
{
    public static class ProgramExtension
    {
        public  static async Task<IClusterClient> StartClient()
        {
            var attempt = 0;
            var attemptsBeforeFailing = 10;
            IClusterClient client;

            while (true)
            {
                try
                {
                    client = new ClientBuilder()
                        // Clustering information 
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "morstead-dev";
                            options.ServiceId = "vs-rules";
                        })
                         // Clustering provider
                         .UseLocalhostClustering()
                         .Build();
                    await client.Connect();
                    break;
                }
                catch (SiloUnavailableException ex)
                {
                    attempt++;

                    if (attempt > attemptsBeforeFailing)
                    {
                        throw ex;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                catch (ConnectionFailedException ex)
                {
                    attempt++;

                    if (attempt > attemptsBeforeFailing)
                    {
                        throw ex;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }

            return client;
        }
    }
}
