using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFileFormElementData : IFormElementSingleValueDate
    {
        IEnumerable<IFormFile> Files { get; set; }
        string ButtonText { get; set; }
        string RemoveText { get; set; }
    }
}