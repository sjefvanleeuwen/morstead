using System.IO;
using Vs.Rules.Core;
using Xunit;

namespace Vs.Rules.Compiler.Tests
{
    public class CodeGeneratorTests
    {
        private CodeGeneratorContext _context = new CodeGeneratorContext()
        {
            CodeGeneratorType = CodeGeneratorTypes.CSharpOrleans,
            Namespace = "VirtualSociety.Unitest",
        };

        public CodeGeneratorTests()
        {
            var controller = new YamlScriptController();
            _context.ParseResult = controller.Parse(File.OpenText(@"../../../minimal.yaml").ReadToEnd());
        }

        public CodeGenerator GetGenerator()
        {
            return new CodeGenerator(_context);
        }

        [Fact]
        public void ShouldGenerateNamespace()
        {
            var gen = GetGenerator();
            gen.BeginNameSpace();
            Assert.Equal(gen.Code,$"namespace {_context.Namespace}{{\r\n");
        }

        [Fact]
        public void ShouldGenerateBeginExecuteMethod()
        {
            var gen = GetGenerator();
            gen.BeginExecuteMethod();
            Assert.Equal(gen.Code, "public async Task<Tuple<List<Question>,zorgtoeslag_state>> Execute(Answer[] answers) {\r\n");
        }

        [Fact]
        public void ShouldInitializeQuestions()
        {
            var gen = GetGenerator();
            gen.InitializeQuestions();
            Assert.Equal(gen.Code, "_questions = new List<Question>();\r\n");
        }

        [Fact]
        public void ShouldGenerateSingleFlowStepSituationalBody()
        {
            var gen = new CodeGenerator(new CodeGeneratorContext()
            {
                CodeGeneratorType = CodeGeneratorTypes.CSharpOrleans,
                Namespace = "VirtualSociety.Unitest"
            });
            gen.FlowStepSituationalBody(new string[] { "a" });
            Assert.Equal(gen.Code, "if (!state.a.Value) return;");

        }

        [Fact]
        public void ShouldGenerateMultipleFlowStepSituationalBody()
        {
            var gen = new CodeGenerator(new CodeGeneratorContext()
            {
                CodeGeneratorType = CodeGeneratorTypes.CSharpOrleans,
                Namespace = "VirtualSociety.Unitest"
            });
            gen.FlowStepSituationalBody(new string[] { "a","b" });
            Assert.Equal(gen.Code, "if (!state.a.Value && !state.b.Value) return;");

        }


    }
}
