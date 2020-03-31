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

        private async Task HandleSelection(IFileListEntry[] files)
        {
            var file = files.FirstOrDefault();
            if (file != null)
            {
                RemoveFile(file.Name);

                (_data.Files as List<IFileListEntry>).Add(file);
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
            }
        }

        private void RemoveFile(string name)
        {
            var fileToRemove = _data.Files.Where(f => f.Name == name).FirstOrDefault();
            if (fileToRemove != null)
            {
                (_data.Files as List<IFileListEntry>).Remove(fileToRemove);
            }
        }
    }
}
