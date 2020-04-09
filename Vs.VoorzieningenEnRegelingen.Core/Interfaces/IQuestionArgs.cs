namespace Vs.VoorzieningenEnRegelingen.Core.Interfaces
{
    public interface IQuestionArgs
    {
        IParametersCollection Parameters { get; }
        string SessionId { get; }
    }
}