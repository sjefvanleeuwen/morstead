using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Vs.Cms.Core
{
    public class PublicatieOverzicht
    {
        public static string Sql = @"SELECT publicatie.ID as publicatieId,
       beheert.moment as beheermoment, 
       reviewer.name as reviewer,
       reviewer.ID as reviewerId,
       akkordeerder.name as akkordeerder,
       akkordeerder.ID as akkordeerderId,
       akkordeert.moment as akkorderingsmoment,
       reviewed.moment as reviewmoment
FROM persoon,beheert, publicatie, persoon reviewer, reviewed, persoon akkordeerder, akkordeert
WHERE MATCH (persoon-(beheert)->publicatie AND reviewer-(reviewed)->publicatie AND akkordeerder-(akkordeert)->publicatie)
AND persoon.id = @beheerderId ORDER BY akkorderingsmoment,reviewmoment DESC OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY";

        public DateTime Beheermoment { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public string Akkordeerder { get; set; }
        public int AkkordeerderId { get; set; }
        public DateTime Akkorderingsmoment { get; set; }
        public DateTime ReviewMoment { get; set; }

        public static async Task<IEnumerable<PublicatieOverzicht>> HaalOp(
            int beheerderId, int regel, int paginaGrootte = 50)
        {
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                return
                    (await conn.QueryAsync<PublicatieOverzicht>(PublicatieOverzicht.Sql,
                        new { beheerderId = beheerderId, regel = regel, paginaGrootte = paginaGrootte }));
            }
        }
    }
}
