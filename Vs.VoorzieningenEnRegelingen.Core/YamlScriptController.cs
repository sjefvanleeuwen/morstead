using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static class StringHelpers
    {
        private static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }

        public static object Infer(this object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.GetType() != typeof(object))
            {
                if (value.GetType() == typeof(int))
                    value = double.Parse(value.ToString(), new CultureInfo("en-US"));
                return value;
            }
            if (value.ToString().In("ja", "yes", "true", "nee", "no", "false"))
            {
                if (value.ToString().In("ja", "yes", "true"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    return double.Parse(value.ToString(), new CultureInfo("en-US"));
                }
                catch (FormatException)
                {
                    return value.ToString();
                }
            }
        }

    }

    public class YamlScriptController
    {
        private const string Ok = "OK";
        private Model.Model _model;
        /// <summary>
        /// Some global culture info's for number conversions (do not make this configurable, 
        /// otherwise it will possibly interfere with script syntax)
        /// </summary>
        private static readonly CultureInfo _numberCulture = CultureInfo.InvariantCulture;

        public YamlScriptController()
        {
        }

        public double Lookup(string tableName, string lookupValue, string lookupColumn, string resultColumn)
        {
            var table = GetTable(tableName);
            var columnIndex = (from p in table.ColumnTypes where p.Name == lookupColumn select p.Index).First();
            var resultColumnIndex = (from p in table.ColumnTypes where p.Name == resultColumn select p.Index).First();
            var value = (from p in table.Rows where p.Columns[columnIndex].Value.ToString() == lookupValue select p.Columns[resultColumnIndex]).FirstOrDefault();
            return double.Parse(value.Value.ToString(),_numberCulture);
        }

        public Formula GetFormula(string name)
        {
            return _model.Formulas.First(p => p.Name == name);
        }

        public Function GetSituation(string formula, string situation)
        {
            return _model.Formulas.First(p=>p.Name== formula).Functions.First(p => p.Situation == situation);
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
                _model = new Model.Model(parser.Formulas(), parser.Tabellen(), parser.Flow());
            }
            catch (Exception ex)
            {
                var result = new ParseResult();
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

        /// <summary>
        /// Execute Workflow
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ExecutionResult ExecuteWorkflow(ref ParametersCollection parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            ExecutionResult executionResult = new ExecutionResult();
            try
            {
                foreach (var step in _model.Steps)
                {
                    // indicates if the step should be executed or skipped.
                    bool match = false;
                    if (!string.IsNullOrEmpty(step.Situation))
                    {
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
                            var context = new FormulaExpressionContext(ref _model, ref parameters, formula);
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
                    }
                    // unconditional step
                    else
                    {
                        match = true;
                    }
                    // execute step.
                    if (match)
                    {
                        executionResult.Stacktrace.Add(new FlowExecutionItem(step, null));
                        // resolve parameter value from named formula.
                        if (string.IsNullOrEmpty(step.Formula))
                        {
                            throw new StepException($"Expected reference to a formula to evaluate in worflow step {step.Name} {step.Description} to execute.", step);
                        }
                        // calculate formula and add it to the parameter list.
                        var formula = GetFormula(step.Formula);
                        var context = new FormulaExpressionContext(ref _model, ref parameters, formula);
                        var result = context.Evaluate();
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

    public class ParametersCollection : List<Parameter>
    {
        public Parameter GetParameter(string name)
        {
            return (from p in this where p.Name == name select p).SingleOrDefault();
        }
    }

    public class ExecutionResult
    {
        public bool IsError { get; internal set; }
        public string Message { get; internal set; }
        public List<FlowExecutionItem> Stacktrace { get; }

        public ExecutionResult()
        {
            Stacktrace = new List<FlowExecutionItem>();
        }
    }

    public class FlowExecutionItem
    {
        public FlowExecutionItem(Step step, Exception exception = null)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
            Exception = exception;
        }

        public Step Step { get; }
        public Exception Exception { get; }
    }

    public class ParseResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string ExpressionTree { get; set; }
        public Model.Model Model { get; set; }
    }
}
