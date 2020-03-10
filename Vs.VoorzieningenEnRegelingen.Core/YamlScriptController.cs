using Flee.PublicTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Core.Collections.NodeTree;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{

    public class YamlScriptController
    {
        private const string Ok = "OK";
        private Model.Model _model;
        private ParametersCollection _unresolved;
        private ExpressionContext localContext;

        public delegate void QuestionDelegate(FormulaExpressionContext sender, QuestionArgs args);

        /// <summary>
        /// Some global culture info's for number conversions (do not make this configurable, 
        /// otherwise it will possibly interfere with script syntax)
        /// </summary>
        private static readonly CultureInfo _numberCulture = CultureInfo.InvariantCulture;

        public YamlScriptController()
        {
        }

        public QuestionDelegate QuestionCallback { get; set; }

        public double Lookup(string tableName, string lookupValue, string lookupColumn, string resultColumn, double defaultValue)
        {
            var table = GetTable(tableName);
            var columnIndex = (from p in table.ColumnTypes where p.Name == lookupColumn select p.Index).First();
            var resultColumnIndex = (from p in table.ColumnTypes where p.Name == resultColumn select p.Index).First();
            var value = (from p in table.Rows where p.Columns[columnIndex].Value.ToString() == lookupValue select p.Columns[resultColumnIndex]).FirstOrDefault();
            double result = defaultValue;
            double.TryParse(value?.Value.ToString(), NumberStyles.Float, _numberCulture, out result);
            return result;
        }

        public StuurInformatie GetHeader()
        {
            return _model.Header;
        }

        public Formula GetFormula(string name)
        {
            return _model.Formulas.First(p => p.Name == name);
        }

        public Function GetSituation(string formula, string situation)
        {
            return _model.Formulas.First(p => p.Name == formula).Functions.First(p => p.Situation == situation);
        }

        public Table GetTable(string name)
        {
            return _model.Tables.First(p => p.Name == name);
        }

        public ParseResult Parse(string yaml)
        {
            try
            {
                YamlParser parser = new YamlParser(yaml, null);
                _model = new Model.Model(parser.Header(), parser.Formulas(), parser.Tabellen(), parser.Flow());
            }
            catch (Exception ex)
            {
                var result = new ParseResult();
                result.IsError = true;
                result.Message = ex.Message;
                return result;
            }
            return new ParseResult()
            {
                Message = Ok,
                ExpressionTree = new YamlDotNet.Serialization.Serializer().Serialize(_model),
                Model = _model
            };
        }

        private bool EvaluateSituation(ref IParametersCollection parameters, IStep step)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var match = false;
            // evaluate if the situation (condition) is appropiate, otherwise skip it.
            // not in input parameters. Evaluate the
            var parameter = parameters.GetParameter(step.Situation);
            if (parameter == null)
            {
                // resolve parameter value from named formula.
                var formula = GetFormula(step.Situation);
                if (formula == null)
                    throw new StepException($"can't resolve parameter '${parameter.Name}' to formula", step);
                // execute the formula which adds the outcome to parameters.
                var context = new FormulaExpressionContext(ref _model, ref parameters, formula, QuestionCallback, this);
                var result = context.Evaluate();
                //var result = Execute(ref context, ref formula, ref parameters, ref executionResult);
                if (result.Value.GetType() != typeof(bool))
                {
                    throw new StepException($"Expected situation to evalute to boolean value in worflow step {step.Name} {step.Description} but got value: {result.Value} of type {result.Value.GetType().Name} instead.", step);
                }
                match = (bool)result.Value;
            }
            else
            {
                if ((bool)parameter.Value.Infer())
                {
                    // execute this step.
                    match = true;
                }
            }
            return match;
        }

        private void ExecuteStep(ref IExecutionResult executionResult, ref IParametersCollection parameters, IStep step)
        {
            if (executionResult is null)
            {
                throw new ArgumentNullException(nameof(executionResult));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            executionResult.Stacktrace.Add(new FlowExecutionItem(step, null));
            // resolve parameter value from named formula.
            if (string.IsNullOrEmpty(step.Formula))
            {
                throw new StepException($"Expected reference to a formula to evaluate in worflow step {step.Name} {step.Description} to execute.", step);
            }
            // calculate formula and add it to the parameter list.
            var formula = GetFormula(step.Formula);
            var context = new FormulaExpressionContext(ref _model, ref parameters, formula, QuestionCallback, this);
            context.Evaluate();
            if (!string.IsNullOrEmpty(step.Break))
            {
                var breakContext = new FormulaExpressionContext(
                    ref _model, 
                    ref parameters, 
                    new Formula(formula.DebugInfo, "recht",new List<Function>() { 
                        new Function(formula.DebugInfo, step.Break)
                    }), 
                    QuestionCallback,
                    this);
                breakContext.Evaluate();
            }
            CheckForStopExecution(parameters, executionResult);
        }

        private void CheckForStopExecution(IParametersCollection parameters, IExecutionResult executionResult)
        {
            if (parameters.Any(p => p.Name == "recht" && bool.TryParse(p.Value.ToString(), out bool value) && value == false))
            {
                executionResult.Stacktrace.Last().StopExecution();
            }
        }

        public ParametersCollection GetFunctionTree(YamlScriptController controller, string expression = null)
        {
            localContext = new ExpressionContext(controller);
            localContext.Options.ParseCulture = CultureInfo.InvariantCulture;
            // Allow the expression to use all static public methods of System.Math
            localContext.Imports.AddType(typeof(Math));
            // Allow the expression to use all static overload public methods our CustomFunctions class
            localContext.Imports.AddType(typeof(CustomFunctions));
            var variables = new System.Collections.Generic.Dictionary<string, Type>();
            var parameters = new ParametersCollection();
            localContext.Variables.ResolveVariableType += (object sender, ResolveVariableTypeEventArgs e) =>
            {
                parameters.Add(new Parameter(e.VariableName, 0, null, ref _model));
                variables.Add(e.VariableName, typeof(object));
                e.VariableType = typeof(object);
            };

            if (expression != null)
            {
                try
                {
                    IGenericExpression<object> eDynamic = localContext.CompileGeneric<object>(expression);
                }
                catch
                {

                }
                return parameters;
            }

            foreach (var formula in _model.Formulas)
            {
                try
                {
                    IGenericExpression<object> eDynamic = localContext.CompileGeneric<object>(formula.Functions[0].Expression);
                }
                catch
                {

                }
            }
            return parameters;
        }

        /// <summary>
        /// Execute Workflow
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IExecutionResult ExecuteWorkflow(ref IParametersCollection parameters, ref IExecutionResult executionResult)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            try
            {
                foreach (var step in _model.Steps)
                {
                    // indicates if the step should be executed or skipped.
                    bool match = false;
                    if (!string.IsNullOrEmpty(step.Situation))
                    {
                        match = EvaluateSituation(ref parameters, step);
                    }
                    // unconditional step
                    else
                    {
                        match = true;
                    }
                    // execute step.
                    if (match)
                    {
                        ExecuteStep(ref executionResult, ref parameters, step);
                        if (executionResult != null && executionResult.Stacktrace.Last().IsStopExecution)
                        {
                            break;
                        };
                    }
                }
            }
            catch (StepException ex)
            {
                executionResult.Stacktrace.Add(new FlowExecutionItem(ex.Step, ex));
            }
            return executionResult;
        }
    }
}
