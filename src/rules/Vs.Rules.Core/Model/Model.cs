using System;
using System.Collections.Generic;

namespace Vs.Rules.Core.Model
{
    public class Model
    {
        public StuurInformatie Header { get; }
        public List<Formula> Formulas { get; }
        public List<Table> Tables { get; }
        public List<Step> Steps { get; }

        public Model(StuurInformatie header, List<Formula> formulas, List<Table> tables, List<Step> steps)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            Formulas = formulas ?? throw new ArgumentNullException(nameof(formulas));
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }

        public IEnumerable<Table> GetTablesByName(string name)
        {
            return Tables.FindAll(p => p.Name == name);
        }
    }
}
