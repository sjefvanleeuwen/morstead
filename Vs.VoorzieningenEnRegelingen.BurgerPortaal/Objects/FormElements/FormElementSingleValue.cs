using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementSingleValue : FormElementData, IFormElementSingleValue
    {
        protected string value = string.Empty;
        public override string Value
        {
            get { return value; }
            set
            {
                if (this.value == value)
                {
                    return;
                }
                this.value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
        public EventCallback<string> ValueChanged { get; set; }
    }
}
