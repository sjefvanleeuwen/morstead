using Flee.PublicTypes;
using System;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using static Vs.VoorzieningenEnRegelingen.Core.YamlScriptController;
using System.Text;

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
            OnQuestion = onQuestion ?? throw new ArgumentNullException(nameof(onQuestion));
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

        private static Parameter Evaluate(FormulaExpressionContext caller, ref ExpressionContext context, ref Formula formula, ref ParametersCollection parameters1, QuestionDelegate onQuestion)
        {
            if (parameters1 is null)
            {
                throw new ArgumentNullException(nameof(parameters1));
            }

            IDynamicExpression e = null;
            if (!formula.IsSituational)
            {
                try
                {
                    e = context.CompileDynamic(formula.Functions[0].Expression);
                    var result = e.Evaluate().Infer();
                    Parameter parameter;
                    parameter = new Parameter(formula.Name, result.Infer());
                    parameters1.Add(parameter);
                    if (context.Variables.ContainsKey(parameter.Name))
                    {
                        context.Variables.Remove(parameter.Name);
                    }
                    context.Variables.Add(parameter.Name, parameter.Value.Infer());
                    return parameter;

                }
                catch (ExpressionCompileException)
                {
                    // Function can not evaluate further, before a Question/Answer sequence is fullfilled by the client.
                    throw new UnresolvedException($"Function {formula.Functions[0].Expression} can not evaluate further, before a Question/Answer sequence is fullfilled by the client.");
                }
            }
            else
            {
                foreach (var function in formula.Functions)
                {
                    foreach (var item in parameters1)
                    {
                        if (item.Name == function.Situation)
                        {
                            try
                            {
                                e = context.CompileDynamic(function.Expression);
                                var parameter = new Parameter(formula.Name, e.Evaluate().Infer());
                                parameters1.Add(parameter);
                                if (context.Variables.ContainsKey(parameter.Name))
                                {
                                    context.Variables.Remove(parameter.Name);
                                }
                                context.Variables.Add(parameter.Name, parameter.Value.Infer());
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
                StringBuilder situations = new StringBuilder();
                var parameters = new ParametersCollection();
                foreach (var function in formula.Functions)
                {
                    situations.Append(function.Situation + ",");
                    parameters.Add(new Parameter(function.Situation, UnresolvedType.Situation));
                }
                if (onQuestion == null)
                    throw new Exception($"In order to evaluate variable one of the following situations:  {situations.ToString().Trim(',')}, you need to provide a delegate callback to the client for providing an answer");
                onQuestion(caller, new QuestionArgs("", parameters));
                // situation has to be formulated as an input parameter by the client.
                throw new UnresolvedException($"Can't evaluate formula {formula.Name} for situation. Please specify one of the following situations: {situations.ToString().Trim(',')}.");
            }

        }

        public Parameter Evaluate()
        {
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
                StringBuilder situations = new StringBuilder();
                var parameters = new ParametersCollection();
                foreach (var function in _formula.Functions)
                {
                    situations.Append(function.Situation +",");
                    parameters.Add(new Parameter(function.Situation, UnresolvedType.Situation));
                }
                if (OnQuestion == null)
                    throw new Exception($"In order to evaluate variable one of the following situations:  {situations.ToString().Trim(',')}, you need to provide a delegate callback to the client for providing an answer");
                OnQuestion(this, new QuestionArgs("", parameters));
                // situation has to be formulated as an input parameter by the client.
                throw new UnresolvedException($"Can't evaluate formula {_formula.Name} for situation. Please specify one of the following situations: {situations.ToString().Trim(',')}.");
            }
        }

        private void ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
        {
            // resolve variable from formulas model.
            var recursiveFormula = (from p in _model.Formulas where p.Name == e.VariableName select p).SingleOrDefault();
            if (recursiveFormula != null)
            {
                // this variable is a formula. Recurvsively execute this formula.
                e.VariableValue = FormulaExpressionContext.Evaluate(this, ref _context, ref recursiveFormula, ref _parameters, OnQuestion).Value.Infer();
                //var context = new FormulaExpressionContext(ref _model, ref _parameters, recursiveFormula, OnQuestion);
                //e.VariableValue = context.Evaluate().Value.Infer();
            }
            else
            {
                // this variable is an input variable. Provide a question to the client for an answer.
                if (OnQuestion == null)
                    throw new Exception($"In order to evaluate variable '${e.VariableName}', you need to provide a delegate callback to the client for providing an answer");
                OnQuestion(this, new QuestionArgs("", new ParametersCollection() { new Parameter(e.VariableName, UnresolvedType.Situation) }));
            }
        }

        private void ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
        {
            // resolve variable from formulas model.
            var recursiveFormula = (from p in _model.Formulas where p.Name == e.VariableName select p).SingleOrDefault();
            if (recursiveFormula != null)
            {
                // this variable is a formula. Recurvsively execute this formula.
                // var context = new FormulaExpressionContext(ref _model, ref _parameters, recursiveFormula, OnQuestion);
                e.VariableType = typeof(double);
            }
            else
            {
                // this variable is an input variable. Provide a question to the client for an answer.
                if (OnQuestion == null)
                    throw new Exception($"In order to evaluate variable '${e.VariableName}', you need to provide a delegate callback to the client for providing an answer");
                OnQuestion(this, new QuestionArgs("", new ParametersCollection() { new Parameter(e.VariableName, UnresolvedType.Situation) }));
            }
        }
    }
}
