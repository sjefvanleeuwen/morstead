namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IQuestionArgs
    {
        IParametersCollection Parameters { get; }
        string SessionId { get; }
    }
}