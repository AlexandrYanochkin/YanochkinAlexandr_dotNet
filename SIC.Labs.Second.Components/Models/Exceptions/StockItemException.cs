using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    public class StockItemException : StockException
    {
        public StockItemException()
        {
        }

        public StockItemException(string message) : base(message)
        {
        }

        public StockItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StockItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
