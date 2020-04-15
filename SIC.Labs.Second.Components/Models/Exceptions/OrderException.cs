using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    [Serializable]
    public class OrderException : StockException
    {
        public OrderException()
        {
        }

        public OrderException(string message) : base(message)
        {
        }

        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
