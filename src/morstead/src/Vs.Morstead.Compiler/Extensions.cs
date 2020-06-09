using System.Globalization;

namespace Vs.Morstead.Compiler
{
    public static class Extensions
    {
        public static string ToPascalCase(this string textToChange)
        {
            System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();

            foreach (char c in textToChange)
            {
                // Replace anything, but letters and digits, with space
                if (!char.IsLetterOrDigit(c))
                {
                    resultBuilder.Append(" ");
                }
                else
                {
                    resultBuilder.Append(c);
                }
            }

            string result = resultBuilder.ToString();

            // Make result string all lowercase, because ToTitleCase does not change all uppercase correctly
            result = result.ToLower();

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            return myTI.ToTitleCase(result).Replace(" ", string.Empty);
        }
    }
}
