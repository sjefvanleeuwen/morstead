using System;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class DirectoryItem
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string ItemsGrainId { get; set; }
    }
}
