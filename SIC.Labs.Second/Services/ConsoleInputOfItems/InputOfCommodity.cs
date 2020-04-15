using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfCommodity : IInputOfItem<Commodity>
    {
        public void InputItem(out Commodity item)
        {
            Commodity commodity = new Commodity();
            Console.WriteLine("Input name of commodity:");
            commodity.Name = Console.ReadLine();
            Console.WriteLine("Input price:");
            commodity.Price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Input Id of manufacturer:");
            commodity.ManufacturerId = int.Parse(Console.ReadLine());

            commodity.Validate();


            item = commodity;
        }

    }
}
