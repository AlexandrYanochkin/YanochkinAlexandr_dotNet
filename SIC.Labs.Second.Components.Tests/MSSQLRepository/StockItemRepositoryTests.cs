using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Tests.DataBaseWorkers;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class StockItemRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod]
        public async Task CreateAsync_ShouldCreateStockItem_True()
        {
            //arrange
            StockItem stockItem = new StockItem
            {
                Count = 45,
                StockId = 1,
                CommodityId = 1
            };

            //act
            await DataAccess.StockItems.CreateAsync(stockItem);
            stockItem.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [StockItem]");
            StockItem stockItemForCompare = DataAccess.StockItems.ReadAsync(stockItem.Id).Result;
            await DataAccess.StockItems.DeleteAsync(stockItem.Id);

            //assert
            Assert.IsTrue(stockItem.Equals(stockItemForCompare));
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadStockItem_True()
        {
            //arrange
            StockItem stockItem = new StockItem
            {
                Count = 45,
                StockId = 1,
                CommodityId = 1
            };

            //act
            await DataAccess.StockItems.CreateAsync(stockItem);
            stockItem.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [StockItem]");
            StockItem stockItemForCompare = DataAccess.StockItems.ReadAsync(stockItem.Id).Result;
            await DataAccess.StockItems.DeleteAsync(stockItem.Id);

            //assert
            Assert.IsTrue(stockItem.Equals(stockItemForCompare));
        }

        [TestMethod]
        public async Task UpdatedAsync_ShouldUpdateStockItem_True()
        {
            //arrange
            StockItem stockItem = new StockItem
            {
                Count = 45,
                StockId = 1,
                CommodityId = 1
            };

            //act
            await DataAccess.StockItems.CreateAsync(stockItem);
            stockItem.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [StockItem]");
            stockItem.Count += 45;
            await DataAccess.StockItems.UpdateAsync(stockItem);
            StockItem stockItemForCompare = DataAccess.StockItems.ReadAsync(stockItem.Id).Result;
            await DataAccess.StockItems.DeleteAsync(stockItem.Id);

            //assert
            Assert.IsTrue(stockItem.Equals(stockItemForCompare));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteStockItem_True()
        {
            //arrange
            StockItem stockItem = new StockItem
            {
                Count = 45,
                StockId = 1,
                CommodityId = 1
            };

            //act
            await DataAccess.StockItems.CreateAsync(stockItem);
            stockItem.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [StockItem]");
            await DataAccess.StockItems.DeleteAsync(stockItem.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [StockItem] WHERE [ID] = {stockItem.Id}") == 0);
        }

    }
}
