using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;

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

        public bool ShowElement => Data != null && !string.IsNullOrWhiteSpace(Data.Name);
    }
}
