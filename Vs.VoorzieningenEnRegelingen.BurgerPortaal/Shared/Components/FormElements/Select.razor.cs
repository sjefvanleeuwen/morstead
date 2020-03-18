using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        protected IEnumerable<string> _keys => Options.Keys;

        /// <summary>
        /// In a selectlist there is always a value selected
        /// </summary>
        [Parameter]
        public override string Value
        {
            get
            {
                if (base.Value == string.Empty)
                {
                    return Options.ToList().FirstOrDefault().Key;
                }
                return base.Value;
            }
            set { base.Value = value; }
        }
    }
}
