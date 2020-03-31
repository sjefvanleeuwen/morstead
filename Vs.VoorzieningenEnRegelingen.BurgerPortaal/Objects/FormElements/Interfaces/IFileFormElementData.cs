using BlazorInputFile;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFileFormElementData : IFormElementSingleValueDate
    {
        IEnumerable<IFileListEntry> Files { get; set; }
        string ButtonText { get; set; }
        string RemoveText { get; set; }
    }
}