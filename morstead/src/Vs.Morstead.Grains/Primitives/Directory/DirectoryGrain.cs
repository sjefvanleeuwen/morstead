using Orleans;
using Orleans.Runtime;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;

namespace Vs.Morstead.Grains.Primitives.Directory
{
    public class DirectoryGrain : Grain, IDirectoryGrain
    {
        private IPersistentState<DirectoryState> _root;

        public DirectoryGrain([PersistentState("pub-sub", "pub-sub-store")]
            IPersistentState<DirectoryState> root)
        {
            _root = root;
        }

        public override Task OnActivateAsync()
        {
            if (_root.State.Dir == null) {
                // "Format" this volume.
                _root.State.Dir = new NestedDictionary<string, DirectoryItem>();
            }
            return base.OnActivateAsync();
        }

        public async Task<DirectoryItem> CreateDirectory(string path)
        {
            var dir = _root.State.CreateDir(path);
            await _root.WriteStateAsync();
            return dir;
        }

        public Task<DirectoryItem> GetDirectory(string path)
        {
            return Task.FromResult(_root.State.GetDir(path.Split("/", StringSplitOptions.RemoveEmptyEntries)));
        }
    }
}
