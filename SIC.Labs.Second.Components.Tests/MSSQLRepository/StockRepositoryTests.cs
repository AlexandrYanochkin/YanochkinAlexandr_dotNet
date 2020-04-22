using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Tests.DataBaseWorkers;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class StockRepositoryTests
    {
        static StockRepositoryTests()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString); 

        [TestMethod]
        public async Task CreateAsync_ShouldCreateStock_True()
        {
            //arrange
            Stock stock = new Stock
            {
                Name = "TestStock",
                Address = "TestAddress",
                Allowance = 0.45
            };

            //act
            await DataAccess.Stocks.CreateAsync(stock);
            stock.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Stock]");
            Stock stockForCompare = DataAccess.Stocks.ReadAsync(stock.Id).Result;
            await DataAccess.Stocks.DeleteAsync(stock.Id);

            //assert
            Assert.IsTrue(stock.Equals(stockForCompare));
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadStock_True()
        {
            //arrange
            Stock stock = new Stock
            {
                Name = "TestStock",
                Address = "TestAddress",
                Allowance = 0.45
            };

            //act
            await DataAccess.Stocks.CreateAsync(stock);
            stock.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Stock]");
            Stock stockForCompare = DataAccess.Stocks.ReadAsync(stock.Id).Result;
            await DataAccess.Stocks.DeleteAsync(stock.Id);

            //assert
            Assert.IsTrue(stock.Equals(stockForCompare));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateStock_True()
        {
            //arrange
            Stock stock = new Stock
            {
                Name = "TestStock",
                Address = "TestAddress",
                Allowance = 0.45
            };

            //act
            await DataAccess.Stocks.CreateAsync(stock);
            stock.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Stock]");
            stock.Allowance *= 1.5;
            await DataAccess.Stocks.UpdateAsync(stock);
            Stock stockForCompare = DataAccess.Stocks.ReadAsync(stock.Id).Result;
            await DataAccess.Stocks.DeleteAsync(stock.Id);

            //assert
            Assert.IsTrue(stock.Equals(stockForCompare));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteStock_True()
        {
            //arrange
            Stock stock = new Stock
            {
                Name = "TestStock",
                Address = "TestAddress",
                Allowance = 0.45
            };

            //act
            await DataAccess.Stocks.CreateAsync(stock);
            stock.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Stock]");
            await DataAccess.Stocks.DeleteAsync(stock.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Stock] WHERE [ID] = {stock.Id}") == 0);
        }

    }
}
