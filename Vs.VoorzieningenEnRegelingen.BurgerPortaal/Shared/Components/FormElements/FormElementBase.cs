using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public class FormElementBase : ComponentBase, IFormElementBase
    {
        private IFormElementData _data;
        
        [CascadingParameter]
        public IFormElementData CascadedData { get; set; }

        [Parameter]
        public IFormElementData Data { 
            get => CascadedData != null ? CascadedData : _data; 
            set => _data = value; 
        }

        [Parameter]
        public virtual string Value { get => Data.Value; set => Data.Value = value; }
        [Parameter]
        public FormElementSize Size { get; set; }

        public bool ShowElement => Data != null && !string.IsNullOrWhiteSpace(Data.Name);

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Size != FormElementSize.Default)
            {
                Data.Size = Size;
            }
        }
    }
}
