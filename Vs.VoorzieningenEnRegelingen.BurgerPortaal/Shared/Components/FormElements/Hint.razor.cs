using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Hint
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string Text { get; set; }
        
        protected bool _show => !string.IsNullOrWhiteSpace(Text);
    }
}
