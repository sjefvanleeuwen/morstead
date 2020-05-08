using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Orleans;
using Orleans.Runtime;
using Vs.Rules.Core;
using Vs.Rules.Grains.Interfaces;
using Vs.Rules.OrleansTestClient;

namespace OrleansClient
{
    public class HelloWorldClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public HelloWorldClientHostedService(IClusterClient client)
        {
            this._client = client;
        }

        private async void ExecuteRule()
        {
            // example of calling grains from the initialized client
            var ruleWorker = this._client.GetGrain<IRuleWorker>(0);
            ParametersCollection parameters = new ParametersCollection();

            var response = await ruleWorker.Execute(Zorgtoeslag.Body, parameters);
            var result = JsonConvert.SerializeObject(response);

            Console.WriteLine($"{result}");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling Virtual Society rule engine.
            ExecuteRule();

            // example of calling grains from the initialized client
            var friend = this._client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Good morning, my friend!");
            Console.WriteLine($"{response}");

            // example of calling IHelloArchive grqain that implements persistence
            var g = this._client.GetGrain<IHelloArchive>(0);
            response = await g.SayHello("Good day!");
            Console.WriteLine($"{response}");

            response = await g.SayHello("Good evening!");
            Console.WriteLine($"{response}");

            var greetings = await g.GetGreetings();
            Console.WriteLine($"\nArchived greetings: {Utils.EnumerableToString(greetings)}");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
