using System;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    [Serializable]
    public class StepException : Exception
    {
        public StepException(string message, Step step) : base(message)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
        }

        public Step Step { get; }

        public StepException()
        {
            throw new NotImplementedException();
        }

        public StepException(string message) : base(message)
        {
            throw new NotImplementedException();
        }

        public StepException(string message, Exception innerException) : base(message, innerException)
        {
            throw new NotImplementedException();
        }

        protected StepException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
