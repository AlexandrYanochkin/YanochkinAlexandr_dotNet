using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ManufacturerId { get; set; }


        public override bool Equals(object obj)
            => (obj is Commodity commodity &&
                   Name == commodity.Name &&
                   Price == commodity.Price &&
                   ManufacturerId == commodity.ManufacturerId);

        public override int GetHashCode()
            => HashCode.Combine(Name, Price, ManufacturerId);

        public override string ToString()
            => $"{Id};{Name};{Price};{ManufacturerId}";
    }
}
