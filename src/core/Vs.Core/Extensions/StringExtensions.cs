using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vs.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToUpperInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static IEnumerable<char> GetInvalidFileNameCharacters(this string fileName)
        {
            var result = new List<char>();
            var invalidCharacters = Path.GetInvalidFileNameChars();
            if (invalidCharacters.Any(c => fileName.Contains(c)))
            {
                foreach (var c in invalidCharacters)
                {
                    if (fileName.Contains(c))
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }
    }
}
