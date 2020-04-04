namespace Vs.VoorzieningenEnRegelingen.Core.Interface
{
    public interface IQuestionArgs
    {
        IParametersCollection Parameters { get; }
        string SessionId { get; }
    }
}