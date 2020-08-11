using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Definitions.Models;

namespace Vs.Definitions.Repositories
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileInformation>> GetAllFiles();

        Task<string> GetFileContent(string contentId);

        Task<string> WriteFile(string directoryName, string fileName, string content, string contentId = null);
    }
}