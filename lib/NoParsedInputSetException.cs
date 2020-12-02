using System;
using System.Runtime.Serialization;

namespace AOC
{
    [Serializable]
    internal class NoParsedInputSetException : Exception
    {
        public NoParsedInputSetException()
        {
        }

        public NoParsedInputSetException(string message) : base(message)
        {
        }

        public NoParsedInputSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoParsedInputSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}