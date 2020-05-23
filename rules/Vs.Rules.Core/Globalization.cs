using System.Globalization;
using Vs.Rules.Core.Properties;

namespace Vs.Rules.Core
{
    /// <summary>
    /// Globalization for rule engine. Please these only once when the engine is instantiated.
    /// </summary>
    public static class Globalization
    {
        /// <summary>
        /// Sets the keyword resource culture. Please set this only once when the engine is instantiated.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public static void SetKeywordResourceCulture(CultureInfo cultureInfo)
        {
            keywords.Culture = cultureInfo;
        }
    }
}
