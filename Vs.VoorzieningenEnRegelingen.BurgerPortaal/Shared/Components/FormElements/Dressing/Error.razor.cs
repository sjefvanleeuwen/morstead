using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing
{
    public partial class Error
    {
        [Parameter]
        public string Text { get; set; }

        private bool _show => !string.IsNullOrWhiteSpace(Text);
    }
}
