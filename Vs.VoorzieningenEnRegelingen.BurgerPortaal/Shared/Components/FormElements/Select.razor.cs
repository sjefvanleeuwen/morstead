using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        private IMultipleOptionsFormElementData _data => Data as IMultipleOptionsFormElementData;
        protected IEnumerable<string> _keys => _data.Options.Keys;

        /// <summary>
        /// In a selectlist there is always a value selected
        /// </summary>
        [Parameter]
        public override string Value
        {
            get
            {
                if (_data.Value == string.Empty)
                {
                    return _data.Options.ToList().FirstOrDefault().Key;
                }
                return _data.Value;
            }
            set { _data.Value = value; }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get => _data.ValueChanged; set => _data.ValueChanged = value; }
    }
}
