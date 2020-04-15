using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.DAL.MSSQLRepository;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class CommodityRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);


        [TestMethod]
        public void CreateTest()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };
            bool result;

            //act
            DataAccess.Commodities.Create(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT * FROM [Commodity] WHERE " +
                $"[Name] = '{commodity.Name}' AND" +
                $"[Price] = {commodity.Price} AND " +
                $"[ManufacturerId] = {commodity.ManufacturerId}");

            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Commodity] WHERE [ID] = {commodity.Id}") > 0);
            DataAccess.Commodities.Delete(commodity.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReadTest()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            DataAccess.Commodities.Create(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT * FROM [Commodity] WHERE " +
                $"[Name] = '{commodity.Name}' AND" +
                $"[Price] = {commodity.Price} AND " +
                $"[ManufacturerId] = {commodity.ManufacturerId}");

            Commodity commodityForCompare = DataAccess.Commodities.Read(commodity.Id); 
            DataAccess.Commodities.Delete(commodity.Id);

            //assert
            Assert.IsTrue(commodity.Equals(commodityForCompare));
        }

        [TestMethod]
        public void UpdateTest()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };
            bool result;

            //act
            DataAccess.Commodities.Create(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT * FROM [Commodity] WHERE " +
                $"[Name] = '{commodity.Name}' AND" +
                $"[Price] = {commodity.Price} AND " +
                $"[ManufacturerId] = {commodity.ManufacturerId}");


            commodity.Name += "Updated";
            DataAccess.Commodities.Update(commodity);
            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Commodity] WHERE [ID] = {commodity.Id}") > 0);
            DataAccess.Commodities.Delete(commodity.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //arrange
            Commodity commodity = new Commodity
            {
                Name = "TestName",
                Price = 25,
                ManufacturerId = 1
            };

            //act
            DataAccess.Commodities.Create(commodity);
            commodity.Id = SqlWorker.ExecuteScalar<int>($"SELECT * FROM [Commodity] WHERE " +
                $"[Name] = '{commodity.Name}' AND" +
                $"[Price] = {commodity.Price} AND " +
                $"[ManufacturerId] = {commodity.ManufacturerId}");

            DataAccess.Commodities.Delete(commodity.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Commodity] WHERE [ID] = {commodity.Id}") == 0);
        }

    }
}
