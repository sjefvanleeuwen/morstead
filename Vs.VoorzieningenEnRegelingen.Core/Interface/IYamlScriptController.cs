using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core.Interface
{
    public interface IYamlScriptController
    {
        List<ContentNode> ContentNodes { get; }
        YamlScriptController.QuestionDelegate QuestionCallback { get; set; }

        void EvaluateFormulaWithoutQA(ref IParametersCollection parameters, IEnumerable<string> formulas);
        void EvaluateFormulaWithoutQA(ref IParametersCollection parameters, string formula);
        IExecutionResult ExecuteWorkflow(ref IParametersCollection parameters, ref IExecutionResult executionResult);
        Formula GetFormula(string name);
        ParametersCollection GetFunctionTree(YamlScriptController controller, string expression = null);
        StuurInformatie GetHeader();
        Function GetSituation(string formula, string situation);
        Table GetTable(string name);
        double Lookup(string tableName, string lookupValue, string lookupColumn, string resultColumn, double defaultValue);
        ParseResult Parse(string yaml);
    }
}