using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class SemanticKeyTests
    {
        /// <summary>
        /// Resolve to question parameter or constant through recursion for a situation.
        /// </summary>
        /// <param name="expression"></param>
        private void ResolveToQuestion(ref YamlScriptController controller, ref Model.Model model, string name, ref List<ContentNode> items, string situation = null)
        {
            var formula = model.Formulas.Find(p => p.Name == name);
            if (formula == null)
                return;
            // resolve all situations.
            foreach (var function in formula.Functions)
            {
                // Get Questions for function
                var parameters = controller.GetFunctionTree(controller, function.Expression);
                var nodeName = string.Join('.', new[] { situation, function.Situation, name }.Where(s => !string.IsNullOrEmpty(s)));
                // Get Boolean Question for situation (if not previously asked)
                if (function.IsSituational && items.Find(p => p.Name == nodeName) == null /* distinct */)
                {
                    function.SemanticKey = nodeName;
                    items.Add(new ContentNode(nodeName) { IsSituational = function.IsSituational, Situation = function.Situation, Parameter = new Parameter(function.Situation, false, TypeEnum.Boolean, ref model) });
                }
                foreach (var parameter in parameters)
                {
                    if (model.Formulas.Find(p => p.Name == parameter.Name) == null)
                    {
                        parameter.SemanticKey = string.Join('.', new[] { parameter.Name, function.Situation, name }.Where(s => !string.IsNullOrEmpty(s)));
                        // not a formula name, so it resolves to a question, add it to the list
                        items.Add(new ContentNode(parameter.SemanticKey) { IsSituational = function.IsSituational, Situation = function.Situation, Parameter = parameter });
                        // find formula's that use the answer to this question.
                        ResolveToQuestion(ref controller, ref model, parameter.Name, ref items);
                    }
                    else
                    {
                        // Recurse to find next question.
                        ResolveToQuestion(ref controller, ref model, parameter.Name, ref items);
                    }
                }
            }
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void BuildTreeForContent()
        {
            List<ContentNode> items = new List<ContentNode>();
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag3.Body);
            var model = result.Model;
            Assert.False(result.IsError);
            ContentNode tree = new ContentNode(result.Model.Header.Onderwerp);
            var questions = controller.GetFunctionTree(controller);
            foreach (var step in result.Model.Steps)
            {
                ResolveToQuestion(ref controller, ref model, step.Formula, ref items, step.Situation);
                // TODO: Resolve recht/geen recht.
                if (step.Break != null && !string.IsNullOrEmpty(step.Break.Expression))
                {
                    step.Break.SemanticKey = string.Join('.', new[] { "geen_recht", step.Situation, step.Formula }.Where(s => !string.IsNullOrEmpty(s)));
                    ContentNode node = new ContentNode(step.SemanticKey) { IsBreak = true, IsSituational = step.IsSituational, Situation = step.Situation, Parameter = new Parameter(name: "recht", value: null, type: TypeEnum.Boolean, model: ref model) };
                    items.Add(node);
                }
            }


            Assert.True(items.Count == 28);
            // TODO: put flat item list with nodes in a multibranch dependency tree...
            //var serializer = new YamlDotNet.Serialization.Serializer();
            //var yaml = serializer.Serialize(items);

            //List<string> tokens = new List<string>();

            //foreach (var item in items)
            //{
            //    tokens.Add(item.Name);
            //}
            //var tokenYaml = serializer.Serialize(tokens);

        }

    }
}
