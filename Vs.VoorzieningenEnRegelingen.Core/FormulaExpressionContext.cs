using Flee.PublicTypes;
using System;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.Core.Calc;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class FormulaExpressionContext
    {
        private ExpressionContext _context;
        private Model.Model _model;
        private Formula _formula;
        private readonly string _variableName;
        private Parameters _parameters;

        public FormulaExpressionContext(ref Model.Model model, ref Parameters parameters, Formula formula)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _formula = formula ?? throw new ArgumentNullException(nameof(formula));
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
        }

        public Parameter Evaluate()
        {
            IDynamicExpression e = null;
            if (!_formula.IsSituational)
            {
                e = _context.CompileDynamic(_formula.Functions[0].Expression);
            }
            else
            {
                throw new NotImplementedException();

            }
            return new Parameter(_formula.Name, (double)e.Evaluate());
        }

        private void ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
        {
            // resolve variable from formulas model.
            var recursiveFormula = (from p in _model.Formulas where p.Name == e.VariableName select p).SingleOrDefault();
            if (recursiveFormula != null)
            {
                // this variable is a formula. Recurvsively execute this formula.
                var context = new FormulaExpressionContext(ref _model, ref _parameters, recursiveFormula);
                e.VariableValue = context.Evaluate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
        {
            e.VariableType = typeof(double);
        }
    }
}
