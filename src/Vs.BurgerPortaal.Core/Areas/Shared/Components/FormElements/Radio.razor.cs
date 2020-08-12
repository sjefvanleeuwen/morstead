using Blazor.NLDesignSystem;
using Blazor.NLDesignSystem.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public partial class Radio
    {
        private IBooleanFormElementData _data =>
            Data as IBooleanFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(IBooleanFormElementData)}");

        private bool IsHorizontalRadio => _data.Options.Count == 2 && _data.Options.All(o => o.Value.Length <= 10);
        protected IEnumerable<string> _keys => _data.Options.Keys;
        private ItemAlignment ItemAlignment => IsHorizontalRadio ? ItemAlignment.Horizontal : ItemAlignment.Default;

        public override bool HasInput => true;

        public override void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            Data = new BooleanFormElementData();
            Data.FillFromExecutionResult(result, contentController);
        }

        private IEnumerable<RadioItem> RadioItems => BuildRadioItems();

        private IEnumerable<RadioItem> BuildRadioItems()
        {
            var result = new List<RadioItem>();
            foreach (var option in _data.Options)
            {
                result.Add(new RadioItem
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
