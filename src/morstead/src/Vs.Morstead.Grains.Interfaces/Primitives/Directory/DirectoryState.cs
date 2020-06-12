using Itenso.TimePeriod;
using System;
using VirtualSociety.VirtualSocietyDid;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class DirectoryState
    {
        public NestedDictionary<string, DirectoryItem> Dir { get; set; }

        public DirectoryItem CreateDir(string path)
        {
            var s = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
            while (!createDir(s)) { }
            return GetDir(s);
        }

        public DirectoryItem GetDir(string[] path)
        {
            var s = Dir;
            for (int i = 0; i < path.Length; i++)
            {
                if (!s.ContainsKey(path[i]))
                    throw new Exception("directory does not exist");
                s = s[path[i]];
            }
            return s.Value;
        }

        public bool DirExists(string[] path)
        {
            var s = Dir;
            for (int i = 0; i < path.Length; i++)
            {
                if (!s.ContainsKey(path[i]))
                    return false;
                s = s[path[i]];
            }
            return true;
        }

        private bool createDir(string[] path)
        {
            var s = Dir;
            for (int i = 0; i < path.Length; i++)
            {
                if (!s.ContainsKey(path[i]))
                {
                    var nested = new NestedDictionary<string, DirectoryItem>();
                    s.Add(path[i], nested);
                    s[path[i]].Value = new DirectoryItem();
                    s[path[i]].Value.Created = DateTime.Now;
                    s[path[i]].Value.Modified = DateTime.Now;
                    s[path[i]].Value.ItemsGrainId = new Did("mstd:dir").ToString();
                    return false;
                }
                s = s[path[i]];
            }
            return true;
        }
    }
}
