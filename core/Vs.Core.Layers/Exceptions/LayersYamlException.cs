using System;
using System.Runtime.Serialization;

namespace Vs.Core.Layers.Exceptions
{
    [Serializable]
    public class LayersYamlException : Exception
    {
        public LayersYamlException()
        {
        }

        public LayersYamlException(string message) : base(message)
        {
        }

        public LayersYamlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LayersYamlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}