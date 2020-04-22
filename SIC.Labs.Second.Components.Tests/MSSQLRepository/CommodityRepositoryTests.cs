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
    public class CommodityRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod]
        public async Task CreateAsync_ShouldCreateCommodity_True()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            await DataAccess.Commodities.CreateAsync(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT MAX([ID]) FROM [Commodity]");
            Commodity commodityForCompare = DataAccess.Commodities.ReadAsync(commodity.Id).Result;
            await DataAccess.Commodities.DeleteAsync(commodity.Id);

            //assert
            Assert.IsTrue(commodity.Equals(commodityForCompare));
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadCommodity_True()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            await DataAccess.Commodities.CreateAsync(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Commodity]");
            Commodity commodityForCompare = DataAccess.Commodities.ReadAsync(commodity.Id).Result;
            await DataAccess.Commodities.DeleteAsync(commodity.Id);

            //assert
            Assert.IsTrue(commodity.Equals(commodityForCompare));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateCommodity_True()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            await DataAccess.Commodities.CreateAsync(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT MAX([ID]) FROM [Commodity]");
            commodity.Name += "Updated";
            await DataAccess.Commodities.UpdateAsync(commodity);
            Commodity commodityForCompare = DataAccess.Commodities.ReadAsync(commodity.Id).Result;
            await DataAccess.Commodities.DeleteAsync(commodity.Id);

            //assert
            Assert.IsTrue(commodity.Equals(commodityForCompare));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteCommodity_True()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            await DataAccess.Commodities.CreateAsync(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT MAX([ID]) FROM [Commodity]");
            await DataAccess.Commodities.DeleteAsync(commodity.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Commodity] WHERE [ID] = {commodity.Id}") == 0);
        }

    }
}




