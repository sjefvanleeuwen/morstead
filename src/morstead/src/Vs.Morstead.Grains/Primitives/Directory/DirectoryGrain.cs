using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualSociety.VirtualSocietyDid;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;

namespace Vs.Morstead.Grains.Primitives.Directory
{
    public class DirectoryGrain : Grain, IDirectoryGrain
    {
        private IPersistentState<DirectoryState> _root { get; set; }

        public DirectoryGrain([PersistentState("directory", "dir-store")]
            IPersistentState<DirectoryState> root)
        {
            _root = root;
        }

        public override Task OnActivateAsync()
        {
            if (_root.State.Dir == null) {
                // "Format" this volume.
                _root.State.Dir = new Dictionary<string, DirectoryItem>();
            }
            return base.OnActivateAsync();
        }

        public async Task<DirectoryItem> CreateDirectory(string path)
        {
            if (_root.State.Dir.ContainsKey(path))
                throw new Exception($"Can't create directory {path}, it already exists.");
            var directoryItem = new DirectoryItem() { Created = DateTime.UtcNow, Modified = DateTime.UtcNow, ItemsGrainId = new Did("mstd:dir").ToString() };
            _root.State.Dir.Add(path, directoryItem);
            await _root.WriteStateAsync();
            return directoryItem;
        }

        public async Task<DirectoryItem> GetDirectory(string path)
        {
            if (!_root.State.Dir.ContainsKey(path))
                throw new Exception($"Can't find directory {path}, it doesn't exist.");
            return _root.State.Dir[path];
        }

        public Task<bool> DirectoryExists(string path)
        {
            return Task.FromResult(_root.State.Dir.ContainsKey(path));
        }

        public async Task SetVolumeName(string name)
        {
            _root.State.VolumeName = name;
            await _root.WriteStateAsync();
        }
    }
}
