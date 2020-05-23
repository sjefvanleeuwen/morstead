using NMemory;
using NMemory.Indexes;
using NMemory.Tables;
using Vs.Rules.Network.Semantic.Entities;

namespace Vs.Rules.Network.Semantic
{

    public class RuleDatabase : Database
    {
        public ITable<ChoiceEntity> Choices { get; private set; }
        public ITable<StepEntity> Steps { get; private set; }

        public RuleDatabase()
        {
            var choiceTable = Tables.Create(p => p.Pk, new IdentitySpecification<ChoiceEntity>(x => x.Pk, 1, 1));
            var stepTable = Tables.Create(p => p.Pk, new IdentitySpecification<StepEntity>(x => x.Pk, 1, 1));
            var choiceIndex = choiceTable.CreateIndex(new RedBlackTreeIndexFactory(),  p => p.PkStep);
            Tables.CreateRelation(stepTable.PrimaryKeyIndex, choiceIndex, x => x, x => x);
            Choices = choiceTable;
            Steps = stepTable;
        }
    }
}
