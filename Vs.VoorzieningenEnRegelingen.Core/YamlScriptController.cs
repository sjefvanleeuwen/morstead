using Flee.PublicTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core
{

    public class YamlScriptController
    {
        private readonly string Ok = "OK";
        private Model.Model _model;
        private ExpressionContext localContext;
        private List<ContentNode> _contentNodes;

        public List<ContentNode> ContentNodes { get { return _contentNodes; } }

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

        /// <summary>
        /// Resolve to question parameter or constant through recursion for a situation.
        /// </summary>
        /// <param name="expression"></param>
        private void ResolveToQuestion(string name, ref List<ContentNode> items, string situation = null, string stepName = null)
        {
            var formula = _model.Formulas.Find(p => p.Name == name);
            if (formula == null)
                return;
            // resolve all situations.
            foreach (var function in formula.Functions)
            {
                // Get Questions for function
                var parameters = GetFunctionTree(this, function.Expression);
                var nodeName = string.Join('.', new[] { YamlParser.StepFormula, YamlParser.SituationAttribute, situation, function.Situation, name }.Where(s => !string.IsNullOrEmpty(s)));
                // Get Boolean Question for situation (if not previously asked)
                if (function.IsSituational && items.Find(p => p.Name == nodeName) == null /* distinct */)
                {
                    function.SemanticKey = nodeName;
                    var contentNode = new ContentNode(nodeName) { IsSituational = function.IsSituational, Situation = function.Situation, Parameter = new Parameter(function.Situation, false, TypeEnum.Boolean, ref _model) };
                    contentNode.Parameter.SemanticKey = nodeName;
                    contentNode.Situation = function.Situation;
                    items.Add(contentNode);
                }
                foreach (var parameter in parameters)
                {
                    if (_model.Formulas.Find(p => p.Name == parameter.Name) == null)
                    {
                        parameter.SemanticKey = string.Join('.', new[] { YamlParser.FormulaAttribute, parameter.Name, function.Situation, name }.Where(s => !string.IsNullOrEmpty(s)));
                        // not a formula name, so it resolves to a question, add it to the list
                        var contentNode = new ContentNode(parameter.SemanticKey) { IsSituational = function.IsSituational, Situation = function.Situation, Parameter = parameter };
                        contentNode.Situation = function.Situation;
                        items.Add(contentNode);
                        // find formula's that use the answer to this question.
                        ResolveToQuestion(parameter.Name, ref items);
                    }
                    else
                    {
                        // Recurse to find next question.
                        ResolveToQuestion(parameter.Name, ref items);
                    }
                }
            }
        }

        public ParseResult Parse(string yaml)
        {
            _contentNodes = new List<ContentNode>();
            try
            {
                YamlParser parser = new YamlParser(yaml, null);
                _model = new Model.Model(parser.Header(), parser.Formulas().ToList(), parser.Tabellen().ToList(), parser.Flow().ToList());
                //_model.AddFormulas(parser.GetFormulasFromBooleanSteps(_model.Steps));
                List<string> inclusiveSituations = null;
                foreach (var step in _model.Steps)
                {
                    if (step.IsSituational)
                    {
                        inclusiveSituations = step.Situation.Split(',')
                            .Select(x => x.Trim())
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .ToList();
                        foreach (var inclusiveSituation in inclusiveSituations)
                        {
                            var contentNodeStep = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepSituation}.{inclusiveSituation}") { Parameter = new Parameter(step.Name, false, TypeEnum.Step, ref _model) };
                            contentNodeStep.Parameter.SemanticKey = contentNodeStep.Name;
                            contentNodeStep.Situation = inclusiveSituation;
                            _contentNodes.Add(contentNodeStep);
                            step.SemanticKey = contentNodeStep.Name;

                            if (step.Choices != null)
                            {
                                foreach (var choice in step.Choices)
                                {
                                    var contentNode = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepSituation}.{inclusiveSituation}.{YamlParser.StepChoice}.{choice.Situation}") { Parameter = new Parameter(choice.Situation, false, TypeEnum.Boolean, ref _model) };
                                    contentNode.Parameter.SemanticKey = contentNode.Name;
                                    contentNode.Situation = inclusiveSituation;
                                    _contentNodes.Add(contentNode);
                                }
                            }

                            if (!string.IsNullOrEmpty(step.Value))
                            {
                                var contentNode = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepSituation}.{inclusiveSituation}.{step.Value}");
                                contentNode.Parameter = new Parameter(step.Name, null, TypeEnum.Double, ref _model);
                                contentNode.Parameter.SemanticKey = contentNode.Name;
                                contentNodeStep.Situation = inclusiveSituation;
                                _contentNodes.Add(contentNode);
                            }

                            ResolveToQuestion(step.Formula, ref _contentNodes, step.Situation, step.Name);

                            if (step.Break != null && !string.IsNullOrEmpty(step.Break.Expression))
                            {
                                ContentNode node = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepSituation}.{inclusiveSituation}.geen_recht") { IsBreak = true, IsSituational = step.IsSituational, Situation = inclusiveSituation, Parameter = new Parameter(name: "recht", value: null, type: TypeEnum.Boolean, model: ref _model) };
                                step.Break.SemanticKey = node.Name;
                                node.Parameter.SemanticKey = node.Name;
                                contentNodeStep.Situation = inclusiveSituation;
                                _contentNodes.Add(node);
                            }
                        }
                    }
                    else
                    {
                        var contentNodeStep = new ContentNode($"{YamlParser.Step}.{step.Name}") { Parameter = new Parameter(step.Name, false, TypeEnum.Step, ref _model) };
                        contentNodeStep.Parameter.SemanticKey = contentNodeStep.Name;
                        _contentNodes.Add(contentNodeStep);
                        step.SemanticKey = contentNodeStep.Name;

                        if (step.Choices != null)
                        {
                            foreach (var choice in step.Choices)
                            {
                                var contentNode = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepChoice}.{choice.Situation}") { Parameter = new Parameter(choice.Situation, false, TypeEnum.Boolean, ref _model) };
                                contentNode.Parameter.SemanticKey = contentNode.Name;
                                _contentNodes.Add(contentNode);
                            }

                            if (!string.IsNullOrEmpty(step.Value))
                            {
                                var contentNode = new ContentNode($"{YamlParser.Step}.{step.Name}.{YamlParser.StepValue}.{step.Value}");
                                contentNode.Parameter = new Parameter(step.Name, null, TypeEnum.Double, ref _model);
                                contentNode.Parameter.SemanticKey = contentNode.Name;
                                _contentNodes.Add(contentNode);
                            }

                            ResolveToQuestion(step.Formula, ref _contentNodes, step.Situation, step.Name);

                            if (step.Break != null && !string.IsNullOrEmpty(step.Break.Expression))
                            {
                                step.Break.SemanticKey = string.Join('.', new[] { YamlParser.Step, step.Name, "geen_recht" }.Where(s => !string.IsNullOrEmpty(s)));
                                ContentNode node = new ContentNode(step.Break.SemanticKey) { IsBreak = true, IsSituational = step.IsSituational, Situation = step.Situation, Parameter = new Parameter(name: "recht", value: null, type: TypeEnum.Boolean, model: ref _model) };
                                node.Parameter.SemanticKey = step.Break.SemanticKey;
                                _contentNodes.Add(node);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ParseResult
                {
                    IsError = true,
                    Message = ex.Message
                };
                return result;
            }
            ContentNodes.Add(new ContentNode("end") { Parameter = new Parameter("end",null,TypeEnum.Step, ref _model) });
            return new ParseResult()
            {
                Message = Ok,
                ExpressionTree = new YamlDotNet.Serialization.Serializer().Serialize(_model),
                Model = _model
            };
        }

        private bool EvaluateSituation(ref IParametersCollection parameters, IStep step, out IStep stepActiveSituation)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            stepActiveSituation = step.Clone();
            var match = false;
            // evaluate if the situation (condition) is appropiate, otherwise skip it.
            // not in input parameters. Evaluate the
            var parameter = parameters.GetParameter(step.Situation);
            if (step.Situation.Contains(','))
            {
                foreach (var inclusiveSituation in step.Situation.Split(',')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray())
                {
                    parameter = parameters.GetParameter(inclusiveSituation);
                    if (parameter != null && parameter.Type == TypeEnum.Boolean)
                    {
                        stepActiveSituation = step.Clone();
                        stepActiveSituation.Situation = inclusiveSituation;
                        // seach in  content nodes for this parameter in typenum steps
                        var s = ContentNodes.FindAll(p => p.Parameter.Type == TypeEnum.Step);
                        return true;
                    }
                }
                throw new StepException($"Can't resolve any of the inclusive situations ${step.Situation}. One of these parameters must exist and be of boolean type.", step);
            }

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
            var questions = new ParametersCollection();
            if (executionResult is null)
            {
                throw new ArgumentNullException(nameof(executionResult));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            executionResult.Stacktrace.Add(new FlowExecutionItem(step, null));


            // Prescedence is (correct order of evaluation)
            // #1 evaluate step variable (value Question) or evaluate situation (choice Question)
            // #2 evaluate formula
            // #3 evaluate break (recht)

            // Evaluate step variable if available
            // Ask a question for a direct input value from the client, if the parameter is unknown.
            // Note: in inference we assume the input value is of type double.
            // When used in combination with "recht", do not advance to the next step until recht is evaluated.
            // When used in combination with "formule", do not advance to the next step until the formule is evaluated.
            if (!string.IsNullOrEmpty(step.Value) && parameters.GetParameter(step.Value) == null)
            {
                if (QuestionCallback == null)
                    throw new Exception($"In order to evaluate step variable  {step.Value}, you need to provide a delegate callback to the client for providing an answer");
                questions.UpSert(new Parameter(step.Value, 0, TypeEnum.Double, ref _model));
                QuestionCallback(null, new QuestionArgs("", questions));
                // step variable has to be formulated as an input parameter by the client.
                throw new UnresolvedException($"Can't evaluate step variable {step.Value}.");
            }

            if (step.Choices != null)
            {
                bool answered = false;
                foreach (var choice in step.Choices)
                {

                    var situation = parameters.GetParameter(choice.Situation);
                    if (situation == null)
                        questions.UpSert(new Parameter(choice.Situation, false, TypeEnum.Boolean, ref _model));
                    else
                    {
                        if ((bool)situation.Value == true)
                        {
                            answered = true;
                        }
                    }
                }
                if (!answered)
                {
                    QuestionCallback(null, new QuestionArgs("", questions));
                    throw new UnresolvedException($"Choices {string.Join(',', step.Choices.Select(p => p.Situation))} can not evaluate further, before these are answered by the client.");
                }
            }

            if (!string.IsNullOrEmpty(step.Formula))
            {
                var context = new FormulaExpressionContext(ref _model, ref parameters, GetFormula(step.Formula), QuestionCallback, this);
                context.Evaluate();
            }

            // Evaluate recht if available.
            if (step.Break != null && !string.IsNullOrEmpty(step.Break.Expression))
            {
                var breakContext = new FormulaExpressionContext(
                    ref _model,
                    ref parameters,
                    new Formula(step.DebugInfo, "recht", new List<Function>() {
                        new Function(step.DebugInfo, step.Break.Expression)
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
            executionResult.ContentNodes = _contentNodes;
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (executionResult is null)
            {
                throw new ArgumentNullException(nameof(executionResult));
            }

            try
            {
                foreach (var step in _model.Steps)
                {
                    // indicates if the step should be executed or skipped.
                    bool match = false;
                    if (!string.IsNullOrEmpty(step.Situation))
                    {
                        match = EvaluateSituation(ref parameters, step, out IStep stepActiveSituation);
                        executionResult.Step = stepActiveSituation;
                    }
                    // unconditional step
                    else
                    {
                        executionResult.Step = step.Clone();
                        match = true;
                    }
                    // execute step.
                    if (match)
                    {
                        ExecuteStep(ref executionResult, ref parameters, executionResult.Step);
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
