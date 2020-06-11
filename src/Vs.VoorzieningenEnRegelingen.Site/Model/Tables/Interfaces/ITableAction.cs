using System;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces
{
    public interface ITableAction
    {
        Action Action { get; set; }
        string IconName { get; set; }
    }
}