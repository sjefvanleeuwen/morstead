using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces
{
    public interface IYamlStorageController
    {
        Task<IEnumerable<string>> GetYamlFiles();

        void WriteYamlFile(string folderName, string fileName, string content);
    }
}