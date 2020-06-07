using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;

namespace Vs.Morstead.Grains.Primitives.Directory
{
    public class DirectoryContentsGrain : Grain, IDirectoryContentsGrain
    {
        private IPersistentState<DirectoryContentsState> _contents;

        public DirectoryContentsGrain([PersistentState("directory", "dir-store")]
            IPersistentState<DirectoryContentsState> contents)
        {
            _contents = contents;
        }

        public override Task OnActivateAsync()
        {
            if (_contents.State.Items == null)
            {
                _contents.State.Items = new Dictionary<string, DirectoryContentsItem>();
            }
            return base.OnActivateAsync();
        }

        public async Task AddItem(DirectoryContentsItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.GrainId is null)
            {
                throw new ArgumentNullException(nameof(item.GrainId));
            }

            if (item.Interface is null)
            {
                throw new ArgumentNullException(nameof(item.Interface));
            }
            if (item.Created is null)
            {
                item.Created = DateTime.Now;
            }

            if (item.Created is null)
            {
                item.Modified = item.Created;
            }

            _contents.State.Items.Add(item.GrainId, item);
            await _contents.WriteStateAsync();
        }

        public Task<DirectoryContentsState> ListItems()
        {
            return Task.FromResult(_contents.State);
        }
    }
}
