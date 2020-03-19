using Dapper;
using Itenso.TimePeriod;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Vs.Cms.Core.Graph.Edges;
using Vs.Cms.Core.Graph.Nodes;
using Vs.Cms.Core.Graph.Views;
using Vs.Graph.Core;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Vs.Cms.Core.Tests
{
    [TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
    public class PublicatieTests
    {
        private readonly GraphController controller;

        public PublicatieTests()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddYamlFile("config.yaml", optional: false).Build();
            Global.ConnectionString = configuration["Cms:SqlConnection"];
            controller = new GraphController(Global.ConnectionString);
        }

        public class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly List<object[]> _data = new List<object[]>
            {
                new [] {
                    new Persoon(){Id=1, Naam="Joost",  Periode = new TimeRange()},
                    new Persoon(){Id=2, Naam="Henk",   Periode = new TimeRange()},
                    new Persoon(){Id=3, Naam="Ingrid", Periode = new TimeRange()}
                }
            };
        }

        private void createDatabase()
        {
            string sql = @"drop table if exists beheert;
drop table if exists reviewed;
drop table if exists akkordeert;
drop table if exists regelgeving;

drop table if exists persoon;
drop table if exists publicatie;
drop table if exists bestand;

CREATE TABLE persoon (
  Id int IDENTITY(1,1) PRIMARY KEY,
  Naam VARCHAR(100),
  PeriodeBegin DATETIME,
  PeriodeEind DATETIME
) AS NODE;

CREATE TABLE publicatie (
  Id int IDENTITY(1,1) PRIMARY KEY,
  Moment DATETIME
) AS NODE;

CREATE TABLE bestand (
  Id int IDENTITY(1,1) PRIMARY KEY,
  Inhoud NTEXT
) AS NODE

CREATE TABLE akkordeert (Id int IDENTITY(1,1) PRIMARY KEY, moment DATETIME) AS EDGE;
CREATE TABLE reviewed (Id int IDENTITY(1,1) PRIMARY KEY, periodeBegin DATETIME, periodeEind DATETIME) AS EDGE;
CREATE TABLE beheert (Id int IDENTITY(1,1) PRIMARY KEY, periodeBegin DATETIME, periodeEind DATETIME) AS EDGE;
CREATE TABLE regelgeving AS EDGE
";


            using (var connection = new SqlConnection(Global.ConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(sql);
            }
        }
            

        [Theory, Order(1)]
        [Trait("Category", "Integration")]
        [ClassData(typeof(TestData))]
        public async void MaakPersonenAan(Persoon joost, Persoon henk, Persoon ingrid)
        {
            createDatabase();
            Assert.True(await controller.InsertNode(joost) == 1);
            Assert.True(await controller.InsertNode(henk) == 2);
            Assert.True(await controller.InsertNode(ingrid) == 3);
        }

        [Fact, Order(2)]
        [Trait("Category", "Integration")]
        public async void MaakPublicatieAan()
        {
            Assert.True(await controller.InsertNode(new Publicatie() { Id=1,Periode=new TimeRange(DateTime.Now)}) == 1);
        }

        [Theory, Order(3)]
        [Trait("Category", "Integration")]
        [ClassData(typeof(TestData))]
        public async void JoostBeheertDePublicatie(Persoon joost, Persoon henk, Persoon ingrid)
        {
            var publicatie = new Publicatie() { Id = 1, Periode = new TimeRange(DateTime.Now) };
            Assert.True(await controller.InsertEdge(
                new Beheert() { 
                    Id = 1,
                    Periode = new TimeRange(DateTime.Now, DateTime.Now.AddDays(10))
                },  joost, publicatie)==1);
        }

        [Fact, Order(4)]
        [Trait("Category", "Integration")]
        public async void HaalOp()
        {
            var result = await PublicatieOverzicht.HaalOp(1, 0, 1);
            Assert.True(result.Count() == 1);
            result = await PublicatieOverzicht.HaalOp(1, 0);
            Assert.True(result.Count() > 1);
        }
    }
}
