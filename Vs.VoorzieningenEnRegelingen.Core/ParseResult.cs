namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ParseResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string ExpressionTree { get; set; }
        public Model.Model Model { get; set; }
    }
}
