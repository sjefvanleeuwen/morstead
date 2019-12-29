namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static partial class TypeInference
    {
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
