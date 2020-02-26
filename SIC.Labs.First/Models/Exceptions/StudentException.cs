using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Models.Exceptions
{
    public class StudentException : Exception
    {
        public StudentException()
        {
        }

        public StudentException(string message) : base(message)
        {
        }
    }
}
