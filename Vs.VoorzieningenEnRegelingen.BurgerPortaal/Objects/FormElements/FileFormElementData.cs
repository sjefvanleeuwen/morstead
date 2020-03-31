using BlazorInputFile;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
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

        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            base.FillFromExecutionResult(result, contentController);

            ButtonText = "Bestand uploaden";
            RemoveText = "Verwijder";
        }
    }
}
