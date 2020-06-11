using System;
using System.IO;

namespace Vs.Morstead.Bpm.TestData
{
    public class TestFileLoader
    {
        public static string Load(string path)
        {
            var file = $"Vs.Morstead.Bpm.TestData/{path}";
            if (File.Exists($"../../../../{file}"))
            {
                return File.ReadAllText($"../../../../{file}");
            }
            else
            {
                return File.ReadAllText(FindDocument(file));
            }
        }

        private static string FindDocument(string file)
        {
            for (int i = 0; i < 10; i++)
            {
                if (File.Exists(file))
                {
                    return file;
                }
                file = $"../{file}";
            }
            throw new Exception("File not found.");
        }
    }
}
