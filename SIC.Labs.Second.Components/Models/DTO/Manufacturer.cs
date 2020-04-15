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
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }



        public override bool Equals(object obj)
            => (obj is Manufacturer manufacturer &&
                manufacturer.Name == Name &&
                manufacturer.Adress == Adress &&
                manufacturer.PhoneNumber == PhoneNumber);

        public override int GetHashCode()
            => HashCode.Combine(Name, Adress, PhoneNumber);

        public override string ToString()
            => $"{Id};{Name};{Adress};{PhoneNumber}";      
    }
}
