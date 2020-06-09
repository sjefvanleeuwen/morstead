using System;
using System.Globalization;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public partial class Number
    {
        public INumericFormElementData _data =>
            Data as INumericFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(INumericFormElementData)}");

        public override bool HasInput => true;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            AddDecimalsToValue();
        }

        private void AddDecimalsToValue()
        {
            //if value is set and decimals is set
            if (!string.IsNullOrWhiteSpace(_data.Value) && (_data.Decimals ?? -1) <= 0)
            {
                return;
            }

            //prepare the value for parsing
            var value = Data.Value?.Replace(',', '.');
            //continue only if parsable
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double d))
            {
                return;
            }

            //continue only if decimals are optional and there are decimals
            if (_data.DecimalsOptional && Math.Round(d) == d)
            {
                return;
            }

            _data.Value = d.ToString("#.00").Replace('.', ',');
        }

        public override void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            Data = new NumericFormElementData();
            Data.FillFromExecutionResult(result, contentController);
        }
    }
}
