using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Select
    {
        protected IEnumerable<string> _keys => Options.Keys;
    }
}
