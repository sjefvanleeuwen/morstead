using Flee.PublicTypes;
using System;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using static Vs.VoorzieningenEnRegelingen.Core.YamlScriptController;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class FormulaExpressionContext
    {
        private ExpressionContext _context;
        private Model.Model _model;
        private Formula _formula;
        private ParametersCollection _parameters;

        public QuestionDelegate OnQuestion { get; }
        public bool Aborted { get; private set; }

        public static void Map(ref ParametersCollection parameters, VariableCollection variables)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (variables is null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            foreach (var parameter in parameters)
            {
                if (variables.ContainsKey(parameter.Name))
                {

                    variables.Remove(parameter.Name);
                }
                variables.Add(parameter.Name, parameter.Value.Infer());
            }
        }

        public FormulaExpressionContext(ref Model.Model model, ref ParametersCollection parameters, Formula formula, QuestionDelegate onQuestion)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _formula = formula ?? throw new ArgumentNullException(nameof(formula));
            OnQuestion = onQuestion;
            _model = model ?? throw new ArgumentNullException(nameof(model));
            // Define the context of our expression
            _context = new ExpressionContext();
            _context.Options.ParseCulture = CultureInfo.InvariantCulture;
            // Allow the expression to use all static public methods of System.Math
            _context.Imports.AddType(typeof(Math));
            // Allow the expression to use all static overload public methods our CustomFunctions class
            _context.Imports.AddType(typeof(CustomFunctions));
            // this will visit ResolveVariableType
            _context.Variables.ResolveVariableType += ResolveVariableType;
            // this will visit ResolveVariableValue
            _context.Variables.ResolveVariableValue += ResolveVariableValue;
            Map(ref _parameters, _context.Variables);
        }

        public Parameter Evaluate()
        {
            if (_context.Variables.ContainsKey("toetsingsinkomen_aanvrager"))
            {

            }
            IDynamicExpression e = null;
            if (!_formula.IsSituational)
            {
                try
                {
                    e = _context.CompileDynamic(_formula.Functions[0].Expression);
                    var result = e.Evaluate().Infer();
                    Parameter parameter;
                    parameter = new Parameter(_formula.Name, result.Infer());
                    _parameters.Add(parameter);
                    if (_context.Variables.ContainsKey(parameter.Name))
                    {
                        _context.Variables.Remove(parameter.Name);
                    }
                    _context.Variables.Add(parameter.Name, parameter.Value.Infer());
                    return parameter;

                }
                catch (ExpressionCompileException)
                {
                    // Function can not evaluate further, before a Question/Answer sequence is fullfilled by the client.
                    throw new UnresolvedException($"Function {_formula.Functions[0].Expression} can not evaluate further, before a Question/Answer sequence is fullfilled by the client.");
                }
            }
            else
            {
                foreach (var function in _formula.Functions) {
                    foreach (var item in _parameters)
                    {
                        if (item.Name == function.Situation)
                        {
                            try
                            {
                                e = _context.CompileDynamic(function.Expression);
                                var parameter = new Parameter(_formula.Name, e.Evaluate().Infer());
                                _parameters.Add(parameter);
                                if (_context.Variables.ContainsKey(parameter.Name))
                                {
                                    _context.Variables.Remove(parameter.Name);
                                }
                                _context.Variables.Add(parameter.Name, parameter.Value.Infer());
                                return parameter;

                            }
                            catch (ExpressionCompileException)
                            {
                                // Function can not evaluate further, before a Question/Answer sequence is fullfilled by the client.
                                throw new UnresolvedException($"Function {function.Expression} can not evaluate further, before a Question/Answer sequence is fullfilled by the client.");
                            }
                        }
                    }
                }
                throw new ArgumentException($"Can't evaluate formula ${_formula.Name} for situation.");
            }
        }

        private void ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
        {
            // resolve variable from formulas model.
            var recursiveFormula = (from p in _model.Formulas where p.Name == e.VariableName select p).SingleOrDefault();
            if (recursiveFormula != null)
            {
                // this variable is a formula. Recurvsively execute this formula.
                var context = new FormulaExpressionContext(ref _model, ref _parameters, recursiveFormula, OnQuestion);
                e.VariableValue = context.Evaluate().Value.Infer();
            }
            else
            {
                // this variable is an input variable. Provide a question to the client for an answer.
                if (OnQuestion == null)
                    throw new Exception($"In order to evaluate variable '${e.VariableName}', you need to provide a delegate callback to the client for providing an answer");
                OnQuestion(this, new QuestionArgs("", new ParametersCollection() { new Parameter(e.VariableName, "unresolved" )}));
            }
        }

        private void ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
        {
            // resolve variable from formulas model.
            var recursiveFormula = (from p in _model.Formulas where p.Name == e.VariableName select p).SingleOrDefault();
            if (recursiveFormula != null)
            {
                // this variable is a formula. Recurvsively execute this formula.
                var context = new FormulaExpressionContext(ref _model, ref _parameters, recursiveFormula, OnQuestion);
                e.VariableType = typeof(double);
            }
            else
            {
                // this variable is an input variable. Provide a question to the client for an answer.
                if (OnQuestion == null)
                    throw new Exception($"In order to evaluate variable '${e.VariableName}', you need to provide a delegate callback to the client for providing an answer");
                OnQuestion(this, new QuestionArgs("", new ParametersCollection() { new Parameter(e.VariableName, "unresolved") }));
            }
        }
    }
}
