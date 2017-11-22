namespace CurrencyConverter.Library.Core
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException()
        {
        }

        public MatchNotFoundException(string message)
            : base(message)
        {
        }

        public MatchNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MatchNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }    }
}