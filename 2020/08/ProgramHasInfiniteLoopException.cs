using System;
using System.Runtime.Serialization;

namespace AOC.Y2020
{
    [Serializable]
    internal class ProgramHasInfiniteLoopException : Exception
    {
        public ProgramHasInfiniteLoopException()
        {
        }

        public ProgramHasInfiniteLoopException(string message) : base(message)
        {
        }

        public ProgramHasInfiniteLoopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProgramHasInfiniteLoopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public long AccumulatorValue { get; set; }
    }
}