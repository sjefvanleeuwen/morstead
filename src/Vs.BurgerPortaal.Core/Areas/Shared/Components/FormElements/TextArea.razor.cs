using System;
using Vs.CitizenPortal.DataModel.Model.FormElements;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public partial class TextArea
    {
        private ITextFormElementData _data =>
            Data as ITextFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(ITextFormElementData)}");

        public override bool HasInput => true;

        public override void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            Data = new TextFormElementData();
            Data.FillFromExecutionResult(result, contentController);
        }
    }
}