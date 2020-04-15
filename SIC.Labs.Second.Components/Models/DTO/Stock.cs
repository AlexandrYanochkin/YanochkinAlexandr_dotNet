using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public double Allowance { get; set; }


        public override bool Equals(object obj)
            => (obj is Stock stock &&
                   Name == stock.Name &&
                   Address == stock.Address &&
                   Allowance == stock.Allowance);

        public override int GetHashCode()
            => HashCode.Combine(Name, Address, Allowance);

        public override string ToString()
            => ($"{Name};{Address};{Allowance}");
    }
}
