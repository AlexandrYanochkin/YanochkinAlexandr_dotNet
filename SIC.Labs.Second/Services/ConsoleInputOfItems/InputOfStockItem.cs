using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfStockItem : IInputOfItem<StockItem>
    {
        public void InputItem(out StockItem item)
        {
            StockItem stockItem = new StockItem();
            Console.WriteLine("Input Id of commodity:");
            stockItem.CommodityId = int.Parse(Console.ReadLine());
            Console.WriteLine("Input count of commodity:");
            stockItem.Count = int.Parse(Console.ReadLine());
            Console.WriteLine("Input Id of stock:");
            stockItem.StockId = int.Parse(Console.ReadLine());

            stockItem.Validate();

            item = stockItem;    
        }

    }
}
