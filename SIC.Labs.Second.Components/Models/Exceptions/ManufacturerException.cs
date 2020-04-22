using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    [Serializable]
    public class ManufacturerException : StockException
    {
        public ManufacturerException()
        {
        }

        public ManufacturerException(string message) : base(message)
        {
        }

        public ManufacturerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManufacturerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
