using NMemory;
using NMemory.Tables;
using System.Collections.Generic;
using Vs.Core.Diagnostics;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Network.Semantic
{
    public class ChoiceEntity : Choice, IChoice
    {
        public int Pk { get; set; }
    }

    public class StepEntity
    {
        public int Pk { get; set; }

        public int Key { get; set; }

        public string Name { get; set; }

        public string Situation { get; set; }

        public string SemanticKey { get; set; }

    }

    public class RuleDatabase : Database
    {
        public ITable<ChoiceEntity> Choices { get; private set; }
        public ITable<StepEntity> Steps { get; private set; }

        public RuleDatabase()
        {
            var choiceTable = Tables.Create(p => p.Pk, new IdentitySpecification<ChoiceEntity>(x => x.Pk, 1, 1));
            var stepTable = Tables.Create(p => p.Pk, new IdentitySpecification<StepEntity>(x => x.Pk, 1, 1));
            // new RedBlackTreeIndexFactory<Person>(), p => p.GroupId);
            Choices = choiceTable;
            Steps = stepTable;
        }
    }
}
