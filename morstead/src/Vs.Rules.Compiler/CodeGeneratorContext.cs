using Vs.Rules.Core;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Compiler
{
    public class CodeGeneratorContext
    {
        public CodeGeneratorTypes CodeGeneratorType {get;set;}
        public string Namespace { get; set; }
        public ParseResult ParseResult { get; set; }
    }
}
