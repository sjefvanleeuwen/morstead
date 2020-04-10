using System.IO;

namespace Vs.VoorzieningenEnRegelingen.Core.TestData
{
    public class YamlTestFileLoader
    {
        public static string Load(string path)
        {
            return File.ReadAllText($"../../../../Vs.VoorzieningenEnRegelingen.Core.TestData/bin/Debug/netstandard2.0/YamlScripts/{path}");
        }
    }
}
