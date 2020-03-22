using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interface;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        private IListFormElementData _data => Data as IListFormElementData;
        protected IEnumerable<string> _keys => _data.Options.Keys;

        public override void FillDataFromResult(IExecutionResult result)
        {
            Data = new ListFormElementData();
            Data.FillFromExecutionResult(result);
        }
    }
}
