using System;

namespace Vs.Graph.Core.Data
{
    public interface IPeriode
    {
        DateTime PeriodeBegin { get; set; }
        DateTime PeriodeEind { get; set; }
    }
}
