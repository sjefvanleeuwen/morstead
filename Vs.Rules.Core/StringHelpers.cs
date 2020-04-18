using System;
using System.Globalization;
using System.Linq;

namespace Vs.Rules.Core
{
    public static class StringHelpers
    {
        private static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }

        public static object Infer(this object value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (value.GetType() == typeof(int))
                return Convert.ToDouble(value);
            if (value.GetType() != typeof(object))
            {
                if (value.GetType() == typeof(int))
                    return double.Parse(value.ToString(), new CultureInfo("en-US"));
            }
            if (value.ToString().In("ja", "yes", "true", "nee", "no", "false"))
                return value.ToString().In("ja", "yes", "true");
            else
            {
                try
                {
                    return double.Parse(Convert.ToString(value, new CultureInfo("en-US")), new CultureInfo("en-US"));
                }
                catch (FormatException)
                {
                    return value.ToString();
                }
            }
        }
    }
}
