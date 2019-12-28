using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core.Visitor
{
    public class DebugInfoVisitor : ISituationVisitor
    {
        public string Line;
        public string Column;

        public void Visit(FormulaSituation norm)
        {
            norm.Accept(this);
        }

        public void Visit(NormSituation formula)
        {
            formula.Accept(this);
        }
    }
}
