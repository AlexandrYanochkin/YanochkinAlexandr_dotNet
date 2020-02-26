using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Models.Exceptions
{
    public class GradeException : Exception
    {
        public GradeException()
        {
        }

        public GradeException(string message) : base(message)
        {
        }
    }
}
