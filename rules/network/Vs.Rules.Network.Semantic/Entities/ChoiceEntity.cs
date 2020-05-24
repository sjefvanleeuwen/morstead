using Vs.Rules.Core.Model;

namespace Vs.Rules.Network.Semantic.Entities
{
    public class ChoiceEntity : Choice, IChoice
    {
        public int Pk { get; set; }
        public int PkStep { get; set; }
    }
}
