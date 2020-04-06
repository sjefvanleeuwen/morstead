using BlazorInputFile;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class File
    {
        private IFileFormElementData _data => Data as IFileFormElementData;

        public override bool HasInput => true;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private string DisplayFileSize(long size)
        {
            if ((size / 1024f) > 1)
            {
                return size + "B";
            }
            if ((size / 1024f) > 1 && (size / 1024f) < 1024)
            {
                return (size / 1024f).ToString("#,00") + "kB";
            }
            return (size / (1024f * 1024f)).ToString("#,00") + "mB";
        }

        private async Task HandleSelection(IFileListEntry[] files)
        {
            var file = files.FirstOrDefault();
            if (file == null)
            {
                return;
            }

            if (!_data.ValidateUploadedFile(file))
            {
                return;
            }

            _data.RemoveFile(file.Name);

            _data.MakeRoomForNewFile();

            (_data.Files as List<IFileListEntry>).Add(file);

            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);
        }
    }
}
