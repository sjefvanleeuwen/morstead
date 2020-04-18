using System;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static partial class TypeInference
    {
        private static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }

        public static InferenceResult Infer(Type type)
        {
            if (type == null)
                return new InferenceResult(InferenceResult.TypeEnum.Double, 0);
            throw new Exception($"Can't infer type from {type.FullName}");
        }

        public static InferenceResult Infer(Function function)
        {
            if (function.IsSituational)
                return new InferenceResult(InferenceResult.TypeEnum.Boolean, null);
            return new InferenceResult(InferenceResult.TypeEnum.Double, null);
        }
        public static InferenceResult Infer(string inference)
        {
            double floatResult = 50;
            TimeSpan timeSpanResult;
            DateTime dateTimeResult;

            // value = "1,097.63";
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite;
            var culture = new CultureInfo("en-US");
            if (inference.In("ja", "yes", "true", "y", "nee", "no", "false", "n"))
                return new InferenceResult(InferenceResult.TypeEnum.Boolean, inference.In("ja", "yes", "true", "y"));
            if (Double.TryParse(inference, style, culture, out floatResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.Double, floatResult);
            }
            if (TimeSpan.TryParse(inference, out timeSpanResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.TimeSpan, timeSpanResult);
            }
            if (DateTime.TryParse(inference, new CultureInfo("nl-NL"), DateTimeStyles.None, out dateTimeResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.DateTime, dateTimeResult);
            }
            return new InferenceResult(InferenceResult.TypeEnum.String, inference);
        }
    }
}
