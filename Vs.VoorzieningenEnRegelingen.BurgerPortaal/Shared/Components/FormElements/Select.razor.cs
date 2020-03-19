using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        protected IEnumerable<string> _keys => Data.Options.Keys;

        /// <summary>
        /// In a selectlist there is always a value selected
        /// </summary>
        [Parameter]
        public override string Value
        {
            get
            {
                if (Data.Value == string.Empty)
                {
                    return Data.Options.ToList().FirstOrDefault().Key;
                }
                return Data.Value;
            }
            set { Data.Value = value; }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get => Data.ValueChanged; set => Data.ValueChanged = value; }
    }
}
