using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Model
    {
        public List<Formula> Formulas { get; }
        public List<Table> Tables { get; }
        public List<Step> Steps { get; }

        public Model(List<Formula> formulas, List<Table> tables, List<Step> steps)
        {
            Formulas = formulas ?? throw new ArgumentNullException(nameof(formulas));
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }
    }
}
