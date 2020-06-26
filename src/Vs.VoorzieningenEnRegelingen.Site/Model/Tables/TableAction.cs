using System;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables
{
    public class TableAction : ITableAction
    {
        public Action Action { get; set; }
        public string IconName { get; set; }
    }
}