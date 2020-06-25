using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.ProfessionalPortal.Morstead.Client.Models;

namespace Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces
{
    public interface IYamlStorageController
    {
        Task<IEnumerable<FileInformation>> GetYamlFiles();
        
        Task<string> GetYamlFileContent(string contentId);

        Task<string> WriteYamlFile(string directoryName, string fileName, string content, string contentId = null);
    }
}