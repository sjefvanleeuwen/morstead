using System;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{

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

        public ParametersCollection GetInputParameters()
        {
            var parameters = new ParametersCollection();
            // Get all parameters that are not resolved by formulas.
            foreach (var s in _model.Steps)
            {
                if (!s.IsSituational)
                    continue;

                if (_model.Formulas.Any(p => p.Name == s.Situation))
                    continue;

                parameters.Add(new Parameter(s.Situation,string.Empty));
            }

            return parameters;
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

        private bool EvaluateSituation(ref ParametersCollection parameters, Step step)
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
            return match;
        }

        private void ExecuteStep(ref ExecutionResult executionResult, ref ParametersCollection parameters, Step step)
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
            var context = new FormulaExpressionContext(ref _model, ref parameters, formula);
            context.Evaluate();
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
