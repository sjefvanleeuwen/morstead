using System;
using System.Collections.Generic;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Radio
    {
        private bool _isHorizontalRadio => Options.Count == 2 && Options.All(o => o.Value.Length <= 10);
        protected IEnumerable<string> _keys => Options.Keys;
    }
}
