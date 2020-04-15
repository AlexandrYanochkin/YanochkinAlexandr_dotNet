using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIC.Labs.Second.Components.Services.Validators
{
    public static class StockValidators
    {
        public static void Validate(this Stock stock)
        {
            if (stock.Id < 0)
                throw new StockException($"Stocks contains Id which less than zero!!!");
            else if (stock.Allowance < 0 && stock.Allowance > 1)
                throw new StockException($"Stocks has an incorrect allowance!!!");
        }

        public static void Validate(this StockItem stockItem)
        {
            if (stockItem.Id < 0 || stockItem.StockId < 0 || stockItem.CommodityId < 0)
                throw new StockItemException($"StockItem contains Id which less than zero!!!");
            else if (stockItem.Count < 0)
                throw new StockItemException($"StockItem has a negative count!!!");
        }

        public static void Validate(this Order order)
        {
            if (order.Id < 0 || order.StockItemId < 0 || order.EmployeeId < 0)
                throw new OrderException("Order has a negative Id!!!");
            else if (order.CreationDate > order.ModificationDate)
                throw new OrderException("Creation date can't be bigger than Modification date!!!");
            else if (!Enum.IsDefined(typeof(OrderStatus), order.Status))
                throw new OrderException("Status is not defined!!!");
            else if (order.FullPrice < 0)
                throw new OrderException("FullPrice can't be less than zero!!!");                  
        }

        public static void Validate(this Commodity commodity)
        {
            if (commodity.Id < 0 || commodity.ManufacturerId < 0)
                throw new CommodityException($"Commodity {commodity.Name} contains Id which less than zero!!!");
            else if (commodity.Price < 0)
                throw new CommodityException($"Commodity has a negative price!!!");
        }

        public static void Validate(this Employee employee)
        {
            if (employee.Id < 0)
                throw new EmployeeException($"Employee {employee.FullName} has a negative Id!!!");
            else if(employee.Age < 0)
                throw new EmployeeException($"Employee {employee.FullName} has a negative Age!!!");
        }

        public static void Validate(this Manufacturer manufacturer)
        {
            if (manufacturer.Id < 0)
                throw new ManufacturerException($"Manufacturer {manufacturer.Name} has a negative Id!!!");
        }

    }
}
