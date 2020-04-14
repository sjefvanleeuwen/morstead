using Microsoft.AspNetCore.Components;
using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class Pagination
    {
        [Parameter]
        public Action Next { get; set; }
        [Parameter]
        public string NextText { get; set; }
        [Parameter]
        public Action Previous { get; set; }
        [Parameter]
        public string PreviousText { get; set; }
        [Parameter]
        public bool NextDisabled { get; set; }
        [Parameter]
        public bool PreviousDisabled { get; set; }

        private void InvokeNext()
        {
            Next?.Invoke();
        }

        private void InvokePrevious()
        {
            Previous?.Invoke();
        }
    }
}
