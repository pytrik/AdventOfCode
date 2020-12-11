using System;
using System.Runtime.Serialization;

namespace AOC
{
    [Serializable]
    public class NoConverterFoundException : Exception
    {
        public NoConverterFoundException()
        {
        }

        public NoConverterFoundException(string message) : base(message)
        {
        }

        public NoConverterFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoConverterFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}