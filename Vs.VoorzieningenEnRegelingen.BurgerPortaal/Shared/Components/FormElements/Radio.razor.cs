using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Radio
    {
        private IOptionsFormElementData _data => Data as IOptionsFormElementData;
        private bool _isHorizontalRadio => _data.Options.Count == 2 && _data.Options.All(o => o.Value.Length <= 10);
        protected IEnumerable<string> _keys => _data.Options.Keys;

        public override void FillDataFromResult(IExecutionResult result)
        {
            Data = new OptionsFormElementData();
            Data.FillFromExecutionResult(result);
        }
    }
}
