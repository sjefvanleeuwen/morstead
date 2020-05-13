using System.Text;

namespace Vs.Rules.Compiler
{
    public class CodeGenerator
    {
        private StringBuilder _sb = new StringBuilder();
        private readonly CodeGeneratorContext _context;

        public CodeGenerator(CodeGeneratorContext context)
        {
            _context = context;
        }

        public string Code { 
            get
            {
                return _sb.ToString();
            } 
        }

        public void Usings()
        {
            _sb.AppendLine(@"using FastMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
");
        }

        public void BeginNameSpace()
        {
            _sb.AppendLine($"namespace {_context.Namespace}{{");
        }

        public void StateClass()
        {
            _sb.AppendLine("public class State {");
            _sb.AppendLine("// inputs are internals as they are not persisted by this rule grain");
            foreach (var step in _context.ParseResult.Model.Steps)
            {
                foreach (var choice in step.Choices)
                {
                    _sb.AppendLine($"internal bool? {choice.Situation} // choice input {step.Name}");
                }
                if (!string.IsNullOrEmpty(step.Value))
                {
                    _sb.AppendLine($"internal double? {step.Value}  // value input {step.Name}");
                }
                _sb.AppendLine("// outputs, persisting calculations that have been computed by this rule grain");
                foreach (var formula in _context.ParseResult.Model.Formulas)
                {
                    _sb.AppendLine($"public double? {formula.Name} // formula output");
                }
            }
            _sb.AppendLine("}");
        }

        public void BeginGrainClass()
        {
            _sb.AppendLine($"public class SimpleRuleGrain : Orleans.Grain, IRule<State>");
        }

        public void StaticHeaderInformation()
        {
            var m = _context.ParseResult.Model;
            _sb.AppendLine($"public const string hBron = \"{m.Header.Bron}\";");
            _sb.AppendLine($"public const string hDomein = \"{m.Header.Domein}\";");
            _sb.AppendLine($"public const string hPeriode = \"{m.Header.Jaar}\";");
            _sb.AppendLine($"public const string hOnderwerp = \"{m.Header.Onderwerp}\";");
            _sb.AppendLine($"public const string hOrganisatie = \"{m.Header.Organisatie}\";");
            _sb.AppendLine($"public const string hStatus = \"{m.Header.Status}\";");
            _sb.AppendLine($"public const string hType = \"{m.Header.Type}\";");
            _sb.AppendLine($"public const string hVersie = \"{m.Header.Versie}\";");
        }

        public void BeginExecuteMethod()
        {
            _sb.AppendLine("public async Task<Tuple<List<Question>,zorgtoeslag_state>> Execute(Answer[] answers) {");
        }

        public void InitializeQuestions()
        {
            _sb.AppendLine("_questions = new List<Question>();");
        }

        public void AnswerReflection()
        {
            _sb.AppendLine("if (answers != null)");
            _sb.AppendLine("{");
            _sb.AppendLine("for (int i=0;i<answers.Length;i++)");
            _sb.AppendLine("{");
            _sb.AppendLine("_accessor[state, answers[i].Name] = answers[i].Value;");
            _sb.AppendLine("}");
            _sb.AppendLine("}");
        }

        public void FlowStepMethodCall(string stepName)
        {
            _sb.AppendLine($"{stepName}();");
            _sb.AppendLine("if (_questions.Any()) return new Tuple<List<Question>, GrainState>(_questions, state);");
        }

        public void EndExecuteMethod(CodeGeneratorContext context)
        {
            _sb.AppendLine("return new Tuple<List<Question>, GrainState>(_questions, state);");
            _sb.AppendLine("}");
        }

        public void FlowStepBeginMethod(string stepName)
        {
            _sb.AppendLine($"private void {stepName}() {{");
        }

        public void FlowStepEndMethod()
        {
            _sb.AppendLine("}");
        }

        public void FlowStepChoiceBody(string[] choices)
        {
            foreach (var choice in choices)
            {
                _sb.AppendLine($"if (state.{choice} == null)");
                FlowStepAddQuestionBody(choice);
            }
        }

        public void FlowStepAddQuestionBody(string name)
        {
            _sb.AppendLine($"_questions.Add(new Question() {{ Name = \"{name}\" }});");
        }

        public void FlowStepValueBody(string valueName)
        {
            _sb.AppendLine($"if (state.{valueName} == null)");
            FlowStepAddQuestionBody(valueName);
        }

        public void FlowStepSituationalBody(string[] situations)
        {
            _sb.Append("if (");
            for (int i=0;i<situations.Length;i++)
            {
                _sb.Append($"!state.{situations[i]}.Value");
                if (i<situations.Length-1)
                    _sb.Append(" && ");
            }
            _sb.Append(") return;");
        }
    }
}
