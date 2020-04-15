using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SIC.Labs.Second.Components.Models.Exceptions
{
    [Serializable]
    public class EmployeeException : StockException
    {
        public EmployeeException()
        {
        }

        public EmployeeException(string message) : base(message)
        {
        }

        public EmployeeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
