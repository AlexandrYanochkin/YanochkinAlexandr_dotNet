using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Tests.DataBaseWorkers;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class OrderRepositoryTests
    {
        static OrderRepositoryTests()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod]
        public async Task CreateAsync_ShouldCreateOrder_True()
        {
            //arrange
            Order order = new Order
            {
                CreationDate = DateTime.Parse("05-10-2015"),
                ModificationDate = DateTime.Parse("09-25-2015"),
                Count = 45,
                FullPrice = 45.5M,
                Status = OrderStatus.SentToClient,
                EmployeeId = 1,
                StockItemId = 1
            };

            //act
            await DataAccess.Orders.CreateAsync(order);
            order.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Order]");
            Order orderForCompare = DataAccess.Orders.ReadAsync(order.Id).Result;
            await DataAccess.Orders.DeleteAsync(order.Id);

            bool zalupa = order.Equals(orderForCompare);

            //assert
            Assert.IsTrue(zalupa);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadOrder_True()
        {
            //arrange
            Order order = new Order
            {
                CreationDate = DateTime.Parse("05-10-2015"),
                ModificationDate = DateTime.Parse("09-25-2015"),
                Count = 45,
                FullPrice = 45.5M,
                Status = OrderStatus.SentToClient,
                EmployeeId = 1,
                StockItemId = 1
            };

            //act
            await DataAccess.Orders.CreateAsync(order);
            order.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Order]");
            Order orderForCompare = DataAccess.Orders.ReadAsync(order.Id).Result;
            await DataAccess.Orders.DeleteAsync(order.Id);

            //assert
            Assert.IsTrue(order.Equals(orderForCompare));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateOrder_True()
        {
            //arrange
            Order order = new Order
            {
                CreationDate = DateTime.Parse("05-10-2015"),
                ModificationDate = DateTime.Parse("09-25-2015"),
                Count = 45,
                FullPrice = 45.5M,
                Status = OrderStatus.SentToClient,
                EmployeeId = 1,
                StockItemId = 1
            };

            //act
            await DataAccess.Orders.CreateAsync(order);
            order.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Order]");
            order.Count += 45;
            await DataAccess.Orders.UpdateAsync(order);
            Order orderForCompare = DataAccess.Orders.ReadAsync(order.Id).Result;
            await DataAccess.Orders.DeleteAsync(order.Id);

            //assert
            Assert.IsTrue(order.Equals(orderForCompare));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteOrder_True()
        {
            //arrange
            Order order = new Order
            {
                CreationDate = DateTime.Parse("05-10-2015"),
                ModificationDate = DateTime.Parse("09-25-2015"),
                Count = 45,
                FullPrice = 45.5M,
                Status = OrderStatus.SentToClient,
                EmployeeId = 1,
                StockItemId = 1
            };

            //act
            await DataAccess.Orders.CreateAsync(order);
            order.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Order]");
            await DataAccess.Orders.DeleteAsync(order.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Order] WHERE [ID] = {order.Id}") == 0);
        }

    }
}
