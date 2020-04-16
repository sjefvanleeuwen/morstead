using System;
using System.Collections.Generic;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        private IListFormElementData _data =>
            Data as IListFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(IListFormElementData)}");
        protected IEnumerable<string> _keys => _data.Options.Keys;

        public override bool HasInput => true;

        public override void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            Data = new ListFormElementData();
            Data.FillFromExecutionResult(result, contentController);
        }
    }
}
