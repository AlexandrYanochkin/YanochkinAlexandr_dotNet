using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfOrder : IInputOfItem<Order>
    {
        public void InputItem(out Order item)
        {
            Order order = new Order();
            Console.WriteLine("Input creationDate:");
            order.CreationDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Input modificationDate:");
            order.ModificationDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Input count:");
            order.Count = int.Parse(Console.ReadLine());

            Console.WriteLine($"Input OrderStatus:\n\t" +
                $"\n\t0.{OrderStatus.OnProcessing.ToString()}" +
                $"\n\t1.{OrderStatus.SentToClient.ToString()}" +
                $"\n\t2.{OrderStatus.Finished.ToString()}\n");

            OrderStatus statusForSet = (OrderStatus)int.Parse(Console.ReadLine());

            if (!Enum.IsDefined(typeof(OrderStatus), (int)statusForSet))
                throw new ArgumentException("Invalid OrderStatus!");

            order.Status = statusForSet;

            Console.WriteLine("Input EmployeeId:");
            order.EmployeeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Input StockItemId:");
            order.StockItemId = int.Parse(Console.ReadLine());
             
            order.Validate();

            item = order;
        }
    }
}
