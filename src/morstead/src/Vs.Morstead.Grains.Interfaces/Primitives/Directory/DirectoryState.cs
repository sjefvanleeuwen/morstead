using Itenso.TimePeriod;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VirtualSociety.VirtualSocietyDid;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class DirectoryState
    {
        public string VolumeName { get; set; }
        public Dictionary<string, DirectoryItem> Dir { get; set; }
    }
}
