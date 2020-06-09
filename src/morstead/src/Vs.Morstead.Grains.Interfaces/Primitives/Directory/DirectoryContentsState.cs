using System.Collections.Generic;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class DirectoryContentsState
    {
        public Dictionary<string,DirectoryContentsItem> Items { get; set; }
    }
}
