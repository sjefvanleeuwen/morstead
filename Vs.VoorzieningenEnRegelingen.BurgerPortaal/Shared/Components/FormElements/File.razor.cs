using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class File
    {
        private IFileFormElementData _data => Data as IFileFormElementData;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private string DisplayFileSize(long size)
        {
            if ((size / 1024) > 1)
            {
                return size + "B";
            }
            if ((size / 1024) > 1 && (size / 1024) < 1024)
            {
                return (size / 1024).ToString("#,00") + "kB";
            }
            return (size / (1024 * 1024)).ToString("#,00") + "mB";
        }
    }
}
