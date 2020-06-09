//using BlazorInputFile;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
//using Vs.Cms.Core.Controllers.Interfaces;
//using Vs.Rules.Core.Interfaces;

//namespace Vs.BurgerPortaal.Core.Objects.FormElements
//{
//    public class FileFormElementData : FormElementData, IFileFormElementData
//    {
//        private IEnumerable<string> _allowedExtensions;

//        public IEnumerable<IFileListEntry> Files { get; set; } = new List<IFileListEntry>();
//        public string ButtonText { get; set; }
//        public string RemoveText { get; set; }
//        public int MaximumNumberOfFiles { get; set; }
//        public IEnumerable<string> AllowedExtensions { get; set; }
//        public long MaximumFileSize { get; set; }

//        public override void CustomValidate(bool unobtrusive = false)
//        {
//            if (!Files.Any())
//            {
//                ErrorTexts.Add("Er zijn geen bestanden geupload.");
//                if (!unobtrusive)
//                {
//                    IsValid = false;
//                }
//            }
//        }

//        public void RemoveFile(string name)
//        {
//            var fileToRemove = Files.Where(f => f.Name == name).FirstOrDefault();
//            if (fileToRemove != null)
//            {
//                (Files as List<IFileListEntry>).Remove(fileToRemove);
//            }
//        }

//        public void MakeRoomForNewFile()
//        {
//            while (Files.Count() >= MaximumNumberOfFiles && Files.Count() > 0)
//            {
//                (Files as List<IFileListEntry>).Remove(Files.FirstOrDefault());
//            }
//        }

//        public bool ValidateUploadedFile(IFileListEntry file)
//        {
//            IsValid = true;
//            ErrorTexts = new List<string>();

//            var extension = Path.GetExtension(file.Name).ToLower().Replace(".", "");
//            if (!GetAllowedExtensions().Contains(extension))
//            {
//                ErrorTexts.Add($"Het bestand heeft niet de juiste extensie (.{extension}); PDF, Word documenten en afbeeldingen zijn toegestaan. Probeer het opnieuw.");
//                IsValid = false;
//                return false;
//            }

//            if (file.Size > MaximumFileSize)
//            {
//                ErrorTexts.Add($"Het bestand is groter dan de maximaal toegestane grootte ({(MaximumFileSize / (1024f * 1024f)).ToString("#.00", Culture)}MB). Probeer het opnieuw.");
//                IsValid = false;
//                return false;
//            }

//            return true;
//        }

//        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
//        {
//            base.FillFromExecutionResult(result, contentController);

//            ButtonText = "Bestand uploaden";
//            RemoveText = "Verwijder";
//            MaximumNumberOfFiles = 1;
//            AllowedExtensions = new List<string> { "doc", "docx", "pdf", "jpg", "jpeg", "png", "tif", "bmp", "jfif", "tiff", "gif" };
//            MaximumFileSize = 5 * 1024 * 1024; //5 MB
//        }

//        private IEnumerable<string> GetAllowedExtensions()
//        {
//            if (_allowedExtensions == null)
//            {
//                var allowedExtensions = new List<string>();
//                AllowedExtensions.ToList().ForEach(m => allowedExtensions.Add(m.ToLower().Replace(".", "")));
//                _allowedExtensions = allowedExtensions;
//            }

//            return _allowedExtensions;
//        }
//    }
//}