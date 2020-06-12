using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualSociety.VirtualSocietyDid;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;

namespace Vs.ProfessionalPortal.Morstead.Client.Controllers
{
    public class YamlStorageController : IYamlStorageController
    {
        public async Task<IEnumerable<string>> GetYamlFiles()
        {
            var did = new Did("mstd:dir").ToString();
            var directoryGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryGrain>(did);
            var dir = await directoryGrain.GetDirectory("/");
            return new List<string>() as IEnumerable<string>;
        }

        public async void WriteYamlFile(string folderName, string fileName, string content)
        {
            var did = new Did("mstd:dir").ToString();
            var directoryGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryGrain>(did);
            var dir = await directoryGrain.GetDirectory(folderName);
        }
    }
}
