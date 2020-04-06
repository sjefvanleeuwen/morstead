using System;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Number
    {
        private INumericFormElementData _data => Data as INumericFormElementData;

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
