using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class Hint
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Content { get; set; }

    }
}
