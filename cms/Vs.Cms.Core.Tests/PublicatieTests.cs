using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Edges;
using Vs.Cms.Core.Nodes;
using Vs.Graph.Core;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Vs.Cms.Core.Tests
{
    [TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
    public class PublicatieTests
    {
        public class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly List<object[]> _data = new List<object[]>
            {
                new [] {
                    new Persoon(){Id=1, Naam="Joost", PeriodeBegin=DateTime.Now},
                    new Persoon(){Id=2, Naam="Henk",  PeriodeBegin=DateTime.Now},
                    new Persoon(){Id=3, Naam="Ingrid",PeriodeBegin=DateTime.Now}
                }
            };
        }

        [Theory, Order(1)]
        [Trait("Category", "Integration")]
        [ClassData(typeof(TestData))]
        public void MaakPersonenAan(Persoon joost, Persoon henk, Persoon ingrid)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddYamlFile("config.yaml", optional: false)
                .Build();
            Global.ConnectionString = configuration["Cms:SqlConnection"];
            GraphController controller = new GraphController(Global.ConnectionString);
            controller.InsertNode(joost);
            controller.InsertNode(henk);
            controller.InsertNode(ingrid);
        }

        [Fact, Order(2)]
        [Trait("Category", "Integration")]
        public void MaakPublicatieAan()
        {
            GraphController controller = new GraphController(Global.ConnectionString);
            controller.InsertNode(new Publicatie() { Id=1,Moment=DateTime.Now});
        }

        [Theory, Order(3)]
        [Trait("Category", "Integration")]
        [ClassData(typeof(TestData))]
        public void JoostBeheertDePublicatie(Persoon joost, Persoon henk, Persoon ingrid)
        {
            GraphController controller = new GraphController(Global.ConnectionString);
            var publicatie = new Publicatie() { Id = 1 };
            controller.InsertEdge(new Beheert() { Id = 1, PeriodeBegin = DateTime.Now },  joost, new Publicatie() { Id = 1 });
        }

        [Fact, Order(4)]
        [Trait("Category", "Integration")]
        public async void HaalOp()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddYamlFile("config.yaml", optional: false)
                .Build();

            Global.ConnectionString = configuration["Cms:SqlConnection"];
            var result = await PublicatieOverzicht.HaalOp(1, 0, 1);
            Assert.True(result.Count() == 1);
            result = await PublicatieOverzicht.HaalOp(1, 0);
            Assert.True(result.Count() > 1);
        }
    }
}
