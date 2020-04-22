using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }



        public override bool Equals(object obj)
            => (obj is Manufacturer manufacturer &&
                manufacturer.Name == Name &&
                manufacturer.Address == Address &&
                manufacturer.PhoneNumber == PhoneNumber);

        public override int GetHashCode()
            => HashCode.Combine(Name, Address, PhoneNumber);

        public override string ToString()
            => $"{Id};{Name};{Address};{PhoneNumber}";      
    }
}
