using System;
using System.Globalization;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static partial class TypeInference
    {
        public static InferenceResult Infer(string inference)
        {
            double floatResult = 50;
            TimeSpan timeSpanResult;
            DateTime dateTimeResult;

            // value = "1,097.63";
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite;
            var culture = new CultureInfo("en-US");

            if (Double.TryParse(inference, style, culture, out floatResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.Double, floatResult);
            }
            if (TimeSpan.TryParse(inference, out timeSpanResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.TimeSpan, timeSpanResult);
            }
            if (DateTime.TryParse(inference, out dateTimeResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.DateTime, dateTimeResult);
            }
            return new InferenceResult(InferenceResult.TypeEnum.String, dateTimeResult);
        }
    }
}
