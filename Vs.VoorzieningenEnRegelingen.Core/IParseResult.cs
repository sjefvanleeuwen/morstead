
namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IParseResult
    {
        string ExpressionTree { get; set; }
        bool IsError { get; set; }
        string Message { get; set; }
        Model.Model Model { get; set; }
    }
}