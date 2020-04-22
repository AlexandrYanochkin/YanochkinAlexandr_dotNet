using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    [Serializable]
    public class StockException : Exception
    {
        public StockException()
        {
        }

        public StockException(string message) : base(message)
        {
        }

        public StockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
