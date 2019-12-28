namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface ISituationVisitor
    {
        void Visit(FormulaSituation norm);
        void Visit(NormSituation formula);
    }
}