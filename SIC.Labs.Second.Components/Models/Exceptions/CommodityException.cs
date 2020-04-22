using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    [Serializable]
    public class CommodityException : StockException
    {
        public CommodityException()
        {
        }

        public CommodityException(string message) : base(message)
        {
        }

        public CommodityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommodityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
