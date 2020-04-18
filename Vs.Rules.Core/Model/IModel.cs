using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface IModel
    {
        IEnumerable<Formula> Formulas { get; }
        StuurInformatie Header { get; }
        IEnumerable<Step> Steps { get; }
        IEnumerable<Table> Tables { get; }

        void AddFormulas(List<Formula> formulas);
    }
}