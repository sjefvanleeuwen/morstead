using System;
using System.Globalization;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static class TypeInference
    {
        public static InferenceResult Infer(string inference)
        {
            int intResult;
            Decimal floatResult=50;
            TimeSpan timeSpanResult;
            DateTime dateTimeResult;
            int stringResult;

            // value = "1,097.63";
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite;
            var culture = new CultureInfo("en-US");

            if (Decimal.TryParse(inference, style, culture, out floatResult))
            {
                return new InferenceResult(InferenceResult.TypeEnum.Decimal, floatResult);
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

        public class InferenceResult
        {
            public TypeEnum Type { get; }
            public object Value { get; }

            public InferenceResult(TypeEnum type, object value)
            {
                Type = type;
                Value = value;
            }

            public enum TypeEnum
            {
                Decimal,
                TimeSpan,
                DateTime,
                String
            }
        }
    }
}
