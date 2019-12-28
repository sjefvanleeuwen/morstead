using Flee.PublicTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static class StringHelpers
    {
        public static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }
    }

    public class YamlScriptController
    {
        private Model.Model _model;
        /// <summary>
        /// Some global culture info's for number conversions (do not make this configurable, 
        /// otherwise it will possibly interfere with script syntax)
        /// </summary>
        private static readonly CultureInfo _numberCulture = CultureInfo.InvariantCulture;
        /// <summary>
        /// Some global culture info's for date conversions
        /// TODO: make conjfigurable.
        /// </summary>
        private static readonly CultureInfo _dateCulture = new CultureInfo("nl-NL");

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

        public Function GetFormula(string name)
        {
            return _model.Formulas.First(p => p.Name == name).Functions.FirstOrDefault();
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
                Message = "OK",
                ExpressionTree = new YamlDotNet.Serialization.Serializer().Serialize(_model),
                Model = _model
            };
        }

        private static ExpressionContext GetExpressionContext()
        {
            // Define the context of our expression
            ExpressionContext context = new ExpressionContext();
            context.Options.ParseCulture = CultureInfo.InvariantCulture;
            // Allow the expression to use all static public methods of System.Math
            context.Imports.AddType(typeof(Math));
            // Allow the expression to use all static overload public methods our CustomFunctions class
            context.Imports.AddType(typeof(CustomFunctions));
            return context;
        }

        private double Execute(ref ExpressionContext context, ref Function function, ref Parameters parameters, ref ExecutionResult excecutionResult)
        {
            foreach (var parameter in parameters)
            {
                context.Variables.Add(parameter.Name, parameter.Value);
            }

            return 0;
        }

        public ExecutionResult Execute(Parameters parameters)
        {
            ExecutionResult executionResult = new ExecutionResult();
            // needs some performance optimalization @ per instance.
            var expressionContext = GetExpressionContext();

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
                            // execute the formula adn add the outcome to parameters.
                            var result = Execute(ref expressionContext, ref formula, ref parameters, ref executionResult);

                        }
                        else
                        {
                            if (parameter.Value.ToString().In("ja", "yes", "true"))
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

    public class Parameters : List<Parameter>
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
        public bool IsError = false;
        public string Message;
        public string ExpressionTree;
        public Model.Model Model;
    }
}
