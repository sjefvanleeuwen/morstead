using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Radio
    {
        private IMultipleOptionsFormElementData _data => Data as IMultipleOptionsFormElementData;
        private bool _isHorizontalRadio => _data.Options.Count == 2 && _data.Options.All(o => o.Value.Length <= 10);
        protected IEnumerable<string> _keys => _data.Options.Keys;
    }
}
