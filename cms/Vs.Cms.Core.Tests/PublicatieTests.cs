using Microsoft.Extensions.Configuration;
using Xunit;

namespace Vs.Cms.Core.Tests
{
    public class PublicatieTests
    {
      //  [Fact]
        public async void Test1()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddYamlFile("config.yaml", optional: false)
                .Build();

            Global.ConnectionString = configuration.GetSection("Cms")["SqlConnection"];

            var result = await PublicatieOverzicht.HaalOp(1,0,1);
        }
    }
}
