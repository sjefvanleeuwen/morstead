using Vs.Rules.Core.Interfaces;

namespace Vs.Rules.Core
{
    public class ParseResult : IParseResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string ExpressionTree { get; set; }
        public Model.Model Model { get; set; }
    }
}
