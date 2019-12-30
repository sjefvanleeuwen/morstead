using System;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Name = name;
            Value = value.Infer();
        }

        public string Name { get; }
        public object Value { get; }
    }
}
