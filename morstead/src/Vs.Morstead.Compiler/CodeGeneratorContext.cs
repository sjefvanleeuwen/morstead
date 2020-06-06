using Vs.Rules.Core;

namespace Vs.Morstead.Compiler
{
    public class CodeGeneratorContext
    {
        public CodeGeneratorTypes CodeGeneratorType { get; set; }
        public string Namespace { get; set; }
        public ParseResult ParseResult { get; set; }
    }
}
