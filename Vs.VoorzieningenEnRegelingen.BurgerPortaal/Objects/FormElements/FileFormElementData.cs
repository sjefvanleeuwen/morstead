using BlazorInputFile;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FileFormElementData : FormElementSingleValueData, IFileFormElementData
    {
        public IEnumerable<IFileListEntry> Files { get; set; } = new List<IFileListEntry>();
        public string ButtonText { get; set; }
        public string RemoveText { get; set; }
        public int MaximumFiles { get; set; }
        public IEnumerable<string> AllowedExtensions { get; set; }
        public long MaximumFileSize { get; set; }

        public override void Validate(bool unobtrusive = false)
        {
            if (!Files.Any())
            {
                ErrorTexts.Add("Er zijn geen bestanden geupload.");
                if (!unobtrusive)
                {
                    IsValid = false;
                }
            }
        }

        public void RemoveFile(string name)
        {
            var fileToRemove = Files.Where(f => f.Name == name).FirstOrDefault();
            if (fileToRemove != null)
            {
                (Files as List<IFileListEntry>).Remove(fileToRemove);
            }
        }

        public void MakeRoomForNewFile()
        {
            while (Files.Count() >= MaximumFiles && Files.Count() > 0)
            {
                (Files as List<IFileListEntry>).Remove(Files.FirstOrDefault());
            }
        }

        public bool ValidateUploadedFile(IFileListEntry file)
        {
            IsValid = true;
            ErrorTexts = new List<string>();

            if (!AllowedExtensions.Contains(Path.GetExtension(file.Name)))
            {
                ErrorTexts.Add("Het bestand heeft niet de juiste extensie; PDF, Word documenten en afbeeldingen zijn toegestaan. Probeer het opnieuw.");
                IsValid = false;
                return false;
            }

            if (file.Size > MaximumFileSize)
            {
                ErrorTexts.Add($"Het bestand is groter dan de maximaal toegestane grootte ({MaximumFileSize / (1024 * 1024):#.00}MB). ; PDF, Word documenten en afbeeldingen zijn toegestaan. Probeer het opnieuw.");
                IsValid = false;
                return false;
            }

            return true;
        }

        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            base.FillFromExecutionResult(result, contentController);

            ButtonText = "Bestand uploaden";
            RemoveText = "Verwijder";
            MaximumFiles = 1;
            AllowedExtensions = new List<string> { ".doc", ".docx", ".pdf", ".jpg", ".jpeg", ".png", ".tif", ".bmp", ".jfif", ".tiff", ".gif" };
            MaximumFileSize = 5 * 1024 * 1024; //5 MB
        }
    }
}