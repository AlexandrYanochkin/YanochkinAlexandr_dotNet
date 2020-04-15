using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Tests.ReadersWritersTests
{
    [TestClass]
    public class JsonReaderTests
    {
        [TestMethod]
        public void TestForManufacturers()
        {
            //arrange
            List<Manufacturer> manufacturers = new List<Manufacturer>()
            {
                new Manufacturer{ Name = "ManufacturerFirst", Adress = "AdressFirst", PhoneNumber = "+375295329911" },
                new Manufacturer{ Name = "ManufacturerSecond", Adress = "AdressSecond", PhoneNumber = "+375295329922" },
                new Manufacturer{ Name = "ManufacturerThird", Adress = "AdressThird", PhoneNumber = "+375295329933" },
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<Manufacturer>()).Test(@"..\..\..\Files\Json\Manufacturers.json", manufacturers));
        }

        [TestMethod]
        public void TestForEmployees()
        {
            //arrange
            List<Employee> employees = new List<Employee>()
            {
                new Employee{ FullName = "EmployeeFirst", Age = 25, PhoneNumber = "+375295329911" },
                new Employee{ FullName = "EmployeeSecond", Age = 25, PhoneNumber = "+375295329922" },
                new Employee{ FullName = "EmployeeThird", Age = 25, PhoneNumber = "+375295329933" },
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<Employee>()).Test(@"..\..\..\Files\Json\Employees.json", employees));
        }

        [TestMethod]
        public void TestForCommodities()
        {
            //arrange
            List<Commodity> commodities = new List<Commodity>()
            {
                new Commodity{ Name = "CommodityFirst", Price = 25.5M, ManufacturerId = 5 },
                new Commodity{ Name = "CommoditySecond", Price = 20.2M, ManufacturerId = 10 },
                new Commodity{ Name = "CommodityThird", Price = 45.5M, ManufacturerId = 15 },
                new Commodity{ Name = "CommodityFour", Price = 15M, ManufacturerId = 20 },
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<Commodity>()).Test(@"..\..\..\Files\Json\Commodities.json", commodities));
        }

        [TestMethod]
        public void TestForStockItems()
        {
            //arrange
            List<StockItem> stockItems = new List<StockItem>()
            {
                new StockItem { Count = 50, CommodityId = 5, StockId = 10 },
                new StockItem { Count = 25, CommodityId = 1, StockId = 4 },
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<StockItem>()).Test(@"..\..\..\Files\Json\StockItems.json", stockItems));
        }

        [TestMethod]
        public void TestForStocks()
        {
            //arrange
            List<Stock> stocks = new List<Stock>()
            {
                new Stock { Name = "StockFirst", Address = "AdressFirst", Allowance = 0.45 },
                new Stock { Name = "StockSecond", Address = "AdressSecond", Allowance = 0.25 }
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<Stock>()).Test(@"..\..\..\Files\Json\Stock.json", stocks));
        }

        [TestMethod]
        public void TestForOrders()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { CreationDate = DateTime.Parse("24.04.2015"), ModificationDate = DateTime.Parse("24.05.2015"),
                    Count = 55, FullPrice = 25.0M, Status = OrderStatus.Finished, StockItemId = 1, EmployeeId = 1  }
            };

            //assert
            Assert.IsTrue((new JsonDeserializer<Order>()).Test(@"..\..\..\Files\Json\Orders.json", orders));
        }

    }
}
