using Orleans;
using System;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public interface IDirectoryContentsGrain : IGrainWithStringKey
    {
        Task AddItem(DirectoryContentsItem item);
        Task<DirectoryContentsState> ListItems();
    }
}
