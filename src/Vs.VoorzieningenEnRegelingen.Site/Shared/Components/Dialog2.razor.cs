using Microsoft.AspNetCore.Components;
using System;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class Dialog2
    {
        //[Parameter]
        //public string Title { get; set; }
        //[Parameter]
        //public string CloseText { get; set; }
        //[Parameter]
        //public RenderFragment ChildContent { get; set; }
        //[Parameter]
        //public bool ShowOkButton { get; set; }
        //[Parameter]
        //public string OkText { get; set; }
        //[Parameter]
        //public Action OkAction { get; set; }

        //private bool isOpen = false;

        //public void OpenDialog()
        //{
        //    isOpen = true;
        //}

        bool dialogIsOpen = false;
        string name = null;
        string animal = null;
        string dialogAnimal = null;

        public void OpenDialog()
        {
            dialogAnimal = null;
            dialogIsOpen = true;
        }

        void OkClick()
        {
            animal = dialogAnimal;
            dialogIsOpen = false;
        }

    }

}
