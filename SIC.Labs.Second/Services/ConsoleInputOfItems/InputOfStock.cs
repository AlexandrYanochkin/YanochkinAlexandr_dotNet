using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfStock : IInputOfItem<Stock>
    {
        public void InputItem(out Stock item)
        {         
            Stock stock = new Stock();

            Console.WriteLine("Input name of stock:");
            stock.Name = Console.ReadLine();
            Console.WriteLine("Input adress of stock:");
            stock.Address = Console.ReadLine();
            Console.WriteLine("Input stock's allowance:");
            stock.Allowance = double.Parse(Console.ReadLine());

            stock.Validate();
            item = stock;
        }
    }
}
