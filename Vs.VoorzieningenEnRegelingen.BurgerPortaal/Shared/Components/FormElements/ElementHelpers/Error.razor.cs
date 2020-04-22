﻿using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.ElementHelpers
{
    public partial class Error
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private bool Show => ChildContent != null && !string.IsNullOrWhiteSpace(ChildContent.ToString());
    }
}
