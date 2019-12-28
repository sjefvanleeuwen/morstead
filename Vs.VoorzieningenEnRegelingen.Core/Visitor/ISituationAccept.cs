namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface ISituationAccept 
    {
        void Accept(ISituationVisitor visitor);
    }
}
