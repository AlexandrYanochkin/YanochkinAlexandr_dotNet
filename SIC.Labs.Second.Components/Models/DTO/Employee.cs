using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }


        public override bool Equals(object obj)
            => (obj is Employee employee && 
            employee.FullName == FullName && 
            employee.Age == Age && 
            employee.PhoneNumber == PhoneNumber);

        public override int GetHashCode()
            => HashCode.Combine(FullName, Age, PhoneNumber);

        public override string ToString()
            => $"{Id};{FullName};{Age};{PhoneNumber}";
    }
}
