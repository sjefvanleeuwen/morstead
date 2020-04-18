using System;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Core
{
    [Serializable]
    public class StepException : Exception
    {
        public StepException(string message, IStep step) : base(message)
        {
            Step = step as Step ?? throw new ArgumentNullException(nameof(step));
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
