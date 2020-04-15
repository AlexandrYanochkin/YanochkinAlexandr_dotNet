using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfManufacturer : IInputOfItem<Manufacturer>
    {
        public void InputItem(out Manufacturer item)
        {
            Manufacturer manufacturer = new Manufacturer();
            Console.WriteLine("Input manufacturer's name:");
            manufacturer.Name = Console.ReadLine();

            Console.WriteLine("Input manufacturer's adress:");
            manufacturer.Adress = Console.ReadLine();

            Console.WriteLine("Input manufacturer's phone number:");
            manufacturer.PhoneNumber = Console.ReadLine();


            if (string.IsNullOrEmpty(manufacturer.Name) ||
                string.IsNullOrEmpty(manufacturer.Adress) ||
                string.IsNullOrEmpty(manufacturer.PhoneNumber))
                throw new ArgumentException("Incorrect value of manufacturer's field!!!");

            item = manufacturer;
        }
    }
}
