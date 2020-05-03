using System.Collections.Generic;

namespace Vs.BurgerPortaal.Core.Shared.Components.FormElements
{
    public partial class Checkbox
    {
        protected IEnumerable<string> _keys => Options.Keys;
    }
}
