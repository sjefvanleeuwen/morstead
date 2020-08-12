﻿using Blazor.NLDesignSystem.Components;
using System;
using System.Collections.Generic;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
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

        private IEnumerable<SelectItem> SelectItems => BuildSelectItemsFromOptions();

        private IEnumerable<SelectItem> BuildSelectItemsFromOptions()
        {
            var result = new List<SelectItem>();
            foreach (var option in _data.Options)
            {
                result.Add(new SelectItem()
                {
                    Value = option.Key,
                    Description = option.Value,
                    IsDisabled = _data.IsDisabled
                });
            }

            return result;
        }
    }
}
