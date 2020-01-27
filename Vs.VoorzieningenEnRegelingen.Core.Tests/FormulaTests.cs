using Flee.PublicTypes;
using Xunit;
using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using System.Globalization;
using YamlDotNet.Serialization;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using static Vs.VoorzieningenEnRegelingen.Core.YamlScriptController;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{

    public class FormulaTests
    {
        ExpressionContext context;

        public FormulaTests()
        {
            // Define the context of our expression
            context = new ExpressionContext();
            context.Options.ParseCulture = CultureInfo.InvariantCulture;
            // Allow the expression to use all static public methods of System.Math
            context.Imports.AddType(typeof(Math));
            // Allow the expression to use all static overload public methods our CustomFunctions class
            context.Imports.AddType(typeof(CustomFunctions));
        }

        [Fact]
        public void Formula_Simple_Calculations()
        {
            // Define the context of our expression
            ExpressionContext context = new ExpressionContext();
            // Allow the expression to use all static public methods of System.Math
            context.Imports.AddType(typeof(Math));
            context.Variables.ResolveVariableType += Formula_Simple_Calculation_Variables_ResolveVariableType;
            // this will visit Variables_ResolveVariableType
            context.Variables.ResolveVariableValue += Formula_Simple_Calculation_Variables_ResolveVariableValue;
            IGenericExpression<Double> eDynamic = context.CompileGeneric<Double>("sqrt(a) + b");
            // This will visit Variables_ResolveVariableValue
            var result = eDynamic.Evaluate();
            Assert.True(result == 9.64575131106459);
            IGenericExpression<Double> eDynamic1 = context.CompileGeneric<Double>("a + b");
            var result1 = eDynamic1.Evaluate();
            Assert.True(result1 == 14);
            IGenericExpression<bool> eDynamic2 = context.CompileGeneric<bool>("a + 1 > b");
            var result2 = eDynamic2.Evaluate();
            Assert.True(result2);
            IGenericExpression<bool> eDynamic3 = context.CompileGeneric<bool>("a > b");
            var result3 = eDynamic3.Evaluate();
            Assert.False(result3);
        }

        private void Formula_Simple_Calculation_Variables_ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
        {
            e.VariableValue = Convert.ToDouble(7);
        }

        private void Formula_Simple_Calculation_Variables_ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
        {
            e.VariableType = typeof(double);
        }

        [Fact]
        public void Formula_Custom_Function_Duration_Overload()
        {
            VariableCollection variables = context.Variables;
            // Allow the expression to use all static overload public methods our CustomFunctions class
            variables["a"] = new DateTime(2010, 1, 2);
            variables["b"] = new DateTime(2010, 1, 1);
            IGenericExpression<TimeSpan> eDynamic = context.CompileGeneric<TimeSpan>("duration(a,b)");
            // this should visit: CustomFunctions.Duration(DateTime begin, DateTime end)
            var result = eDynamic.Evaluate();
            Assert.True(result == new TimeSpan(1, 0, 0, 0));
            variables["x"] = TimeSpan.FromHours(48);
            variables["y"] = TimeSpan.FromHours(24);
            // this should visit: CustomFunctions.Duration(TimeSpan begin, TimeSpan end)
            IGenericExpression<TimeSpan> eDynamic2 = context.CompileGeneric<TimeSpan>("duration(x,y)");
            result = eDynamic2.Evaluate();
            Assert.True(result == new TimeSpan(1, 0, 0, 0));
        }

        /// <summary>
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000. 
        /// </summary>
        [Fact]
        void Formula_Zorgtoeslag_2019_Rekenvoorbeeld1()
        {
            VariableCollection variables = context.Variables;
            // stap 1
            variables["standaardpremie"] = 1609;
            variables["drempelinkomen"] = 20941;
            variables["toetsingsinkomen_aanvrager"] = 19000;
            variables["toetsingsinkomen_partner"] = 0;
            // stap 2
            IGenericExpression<Double> eDynamic0 = context.CompileGeneric<Double>("toetsingsinkomen_aanvrager + toetsingsinkomen_partner");
            variables["toetsingsinkomen"] = eDynamic0.Evaluate();
            // stap 3
            // Let op!Leidt het tweede deel van de formule tot een negatief bedrag? Reken dan met € 0.
            // using c# math expression min(n,0)
            IGenericExpression <Double> eDynamic = context.CompileGeneric<Double>("min(percentage(2.005) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 1189)");
            variables["normpremie"] = eDynamic.Evaluate();
            // stap 4
            IGenericExpression<Double> eDynamic2 = context.CompileGeneric<Double>("standaardpremie - normpremie");
            variables["zorgtoeslagjaar"] = eDynamic2.Evaluate();
            IGenericExpression<Double> eDynamic3 = context.CompileGeneric<Double>("round(zorgtoeslagjaar/12,2)");
            variables["zorgtoeslagmaand"] = eDynamic3.Evaluate();
            Assert.True((double)variables["zorgtoeslagmaand"] == 99.09);
            IGenericExpression<Double> eDynamic4 = context.CompileGeneric<Double>("round(zorgtoeslagmaand,0)");
            variables["zorgtoeslagmaandafgerond"] = eDynamic4.Evaluate();
            Assert.True((double)variables["zorgtoeslagmaandafgerond"] == 99);
        }

        /// <summary>
        /// Herman(militair) en Marianne(secretaresse) zijn getrouwd. Zijn jaarinkomen is € 26.200. Haar jaarinkomen
        /// is € 7.000.Henk is geen verzekerde voor de Wet op de zorgtoeslag, want hij is militair. Hij kan daarom zelf
        /// geen zorgtoeslag aanvragen.Marianne moet de zorgtoeslag aanvragen en kan 50 % van de berekende
        /// zorgtoeslag krijgen.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        void Formula_Zorgtoeslag_2019_Rekenvoorbeeld2()
        {
            VariableCollection variables = context.Variables;
            // stap 1
            variables["standaardpremie"] = 3218;
            variables["drempelinkomen"] = 20941;
            variables["toetsingsinkomen_aanvrager"] = 7000;
            variables["toetsingsinkomen_partner"] = 26200;
            // stap 2
            IGenericExpression<Double> eDynamic0 = context.CompileGeneric<Double>("toetsingsinkomen_aanvrager + toetsingsinkomen_partner");
            variables["toetsingsinkomen"] = eDynamic0.Evaluate();
            // stap 3
            IGenericExpression<Double> eDynamic = context.CompileGeneric<Double>("round(percentage(4.315) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0),2)");
            variables["normpremie"] = eDynamic.Evaluate();
            Assert.True((double)variables["normpremie"] == 2561.02);
            // stap 4
            IGenericExpression<Double> eDynamic2 = context.CompileGeneric<Double>("(standaardpremie - normpremie) * percentage(50)");
            variables["zorgtoeslagjaar"] = eDynamic2.Evaluate();
            IGenericExpression<Double> eDynamic3 = context.CompileGeneric<Double>("round(zorgtoeslagjaar/12,2)");
            variables["zorgtoeslagmaand"] = eDynamic3.Evaluate();
            Assert.True((double)variables["zorgtoeslagmaand"] == 27.37);
            IGenericExpression<Double> eDynamic4 = context.CompileGeneric<Double>("round(zorgtoeslagmaand,0)");
            variables["zorgtoeslagmaandafgerond"] = eDynamic4.Evaluate();
            Assert.True((double)variables["zorgtoeslagmaandafgerond"] == 27);

            // output variables as YAML.
            var serializer = new SerializerBuilder()
               .Build();

            var json = serializer.Serialize(variables);
        }

        [Fact]
        void Formula_Table_Select_Test()
        {
            // -naam: woonlandfactoren
            //  woonland, factor:
            //  - [Finland, 0.7161]
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            ExpressionContext context = new ExpressionContext(controller);
            //  Tsjechië,            0.2412 
            context.Variables.Add("woonland", "Tsjechië");
            var formula = controller.GetFormula("woonlandfactor");
            //  select(string tableName, string lookupValue, string lookupColumn, string resultColumn)
            IDynamicExpression e = context.CompileDynamic(formula.Functions[0].Expression);
            double result1 = (double)e.Evaluate();
            Assert.True(result1 == 0.2412);
        }


        public event QuestionDelegate OnQuestion;

        [Fact]
        void Formula_Can_Execute_FormulaExpressionContext()
        {

            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
            var result = controller.Parse(YamlZorgtoeslag.Body);
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("woonland","Nederland"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };
            var model = result.Model;
            var context = new FormulaExpressionContext(ref model, ref parameters, controller.GetFormula("normpremie"), controller.QuestionCallback);
            var parameter = context.Evaluate();
            Assert.True(parameter.Name == "normpremie");
            Assert.True((double)parameter.Value == 419.86704999999995);
            Assert.True(parameters.Count == 7);
        }

        [Fact]
        void Formula_Can_Execute_FormulaExpressionContext_UsingQuestionAnswer()
        {

            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("woonland","Nederland"),
                /*new Parameter("toetsingsinkomen_aanvrager",(double)19000),*/  // expect this question in FormulaTests_OnQuestion
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };
            var model = result.Model;
            bool called = false;
            OnQuestion = (FormulaExpressionContext sender, QuestionArgs args) => {
                Assert.True(args.Parameters[0].Name == "toetsingsinkomen_aanvrager");
                called = true;

            };
            var context = new FormulaExpressionContext(ref model, ref parameters, controller.GetFormula("normpremie"), OnQuestion);
            bool unresolvedException = false;
            try
            {
                var parameter = context.Evaluate();
            }
            catch (UnresolvedException)
            {
                unresolvedException = true;
            }
            Assert.True(called);
            Assert.True(unresolvedException);
        }

        [Fact]
        void Formula_Can_Discover_Parameters()
        {
            var localContext = new ExpressionContext();
            var variables = new System.Collections.Generic.Dictionary<string,Type>();
            localContext.Variables.ResolveVariableType += (object sender, ResolveVariableTypeEventArgs e) =>
            {
                variables.Add(e.VariableName,typeof(double));
                e.VariableType = typeof(double);
            };

            IGenericExpression<object> eDynamic = localContext.CompileGeneric<object>("a + b");

            Assert.True(variables.Count==2);
            Assert.True(variables.ContainsKey("a"));
            Assert.True(variables.ContainsKey("b"));

        }
    }
}
