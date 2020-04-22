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
    public class ManufacturerRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod]
        public async Task CreateAsync_ShouldCreateManufacturer_True()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Address = "TestAdress",
                PhoneNumber = "TestPhone"
            };

            //act
            await DataAccess.Manufacturers.CreateAsync(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Manufacturer]");
            Manufacturer manufacturerForCompare = DataAccess.Manufacturers.ReadAsync(manufacturer.Id).Result;
            await DataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

            //assert
            Assert.IsTrue(manufacturer.Equals(manufacturerForCompare));
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadManufacturer_True()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Address = "TestAdress",
                PhoneNumber = "TestPhone"
            };

            //act
            await DataAccess.Manufacturers.CreateAsync(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Manufacturer]");
            Manufacturer manufacturerForCompare = DataAccess.Manufacturers.ReadAsync(manufacturer.Id).Result;
            await DataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

            //assert
            Assert.IsTrue(manufacturer.Equals(manufacturerForCompare));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateManufacturer_True()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Address = "TestAdress",
                PhoneNumber = "TestPhone"
            };

            //act
            await DataAccess.Manufacturers.CreateAsync(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Manufacturer]");
            manufacturer.Name += "Updated";
            await DataAccess.Manufacturers.UpdateAsync(manufacturer);
            Manufacturer manufacturerForCompare = DataAccess.Manufacturers.ReadAsync(manufacturer.Id).Result;
            await DataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

            //assert
            Assert.IsTrue(manufacturer.Equals(manufacturerForCompare));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteManufacturer_True()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Address = "TestAdress",
                PhoneNumber = "TestPhone"
            };
            bool result;

            //act
            await DataAccess.Manufacturers.CreateAsync(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Manufacturer]");
            await DataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Manufacturer] WHERE [ID] = {manufacturer.Id}") == 0);
        }

    }
}


