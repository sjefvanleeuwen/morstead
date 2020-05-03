using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components
{
    public partial class CalculationHeader
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string SubTitle { get; set; }
        [Parameter]
        public int? Number { get; set; }
        [Parameter]
        public string Subject { get; set; }

        private bool ShowTitle => !string.IsNullOrWhiteSpace(Title);
        private bool ShowSubtitle => !string.IsNullOrWhiteSpace(SubTitle);
        private bool ShowNumberedItem => ShowNumber || ShowSubject;
        private bool ShowNumber => (Number ?? 0) != 0;
        private bool ShowSubject => !string.IsNullOrWhiteSpace(Subject);
    }
}
