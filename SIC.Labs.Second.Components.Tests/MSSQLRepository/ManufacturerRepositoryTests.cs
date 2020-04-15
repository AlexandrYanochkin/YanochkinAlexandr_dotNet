using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.DAL.MSSQLRepository;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class ManufacturerRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);


        [TestMethod()]
        public void CreateTest()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Adress = "TestAdress",
                PhoneNumber = "TestPhone"
            };
            bool result;

            //act
            DataAccess.Manufacturers.Create(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Manufacturer] WHERE " +
                $"[Name] = '{manufacturer.Name}' AND " +
                $"[Adress] = '{manufacturer.Adress}' AND" +
                $"[PhoneNumber] = '{manufacturer.PhoneNumber}'");

            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Manufacturer] WHERE [ID] = {manufacturer.Id}") > 0);
            DataAccess.Manufacturers.Delete(manufacturer.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ReadTest()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Adress = "TestAdress",
                PhoneNumber = "TestPhone"
            };
            bool result;

            //act
            DataAccess.Manufacturers.Create(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Manufacturer] WHERE " +
                $"[Name] = '{manufacturer.Name}' AND " +
                $"[Adress] = '{manufacturer.Adress}' AND" +
                $"[PhoneNumber] = '{manufacturer.PhoneNumber}'");

            Manufacturer manufacturerForCompare = DataAccess.Manufacturers.Read(manufacturer.Id);
            DataAccess.Manufacturers.Delete(manufacturer.Id);

            //assert
            Assert.IsTrue(manufacturer.Equals(manufacturerForCompare));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Adress = "TestAdress",
                PhoneNumber = "TestPhone"
            };
            bool result;

            //act
            DataAccess.Manufacturers.Create(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Manufacturer] WHERE " +
                $"[Name] = '{manufacturer.Name}' AND " +
                $"[Adress] = '{manufacturer.Adress}' AND" +
                $"[PhoneNumber] = '{manufacturer.PhoneNumber}'");

            manufacturer.Name += "Updated";
            DataAccess.Manufacturers.Update(manufacturer);
            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Manufacturer] WHERE [ID] = {manufacturer.Id}") > 0);
            DataAccess.Manufacturers.Delete(manufacturer.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //arrange
            Manufacturer manufacturer = new Manufacturer
            {
                Name = "TestName",
                Adress = "TestAdress",
                PhoneNumber = "TestPhone"
            };
            bool result;

            //act
            DataAccess.Manufacturers.Create(manufacturer);
            manufacturer.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Manufacturer] WHERE " +
                $"[Name] = '{manufacturer.Name}' AND " +
                $"[Adress] = '{manufacturer.Adress}' AND" +
                $"[PhoneNumber] = '{manufacturer.PhoneNumber}'");

            DataAccess.Manufacturers.Delete(manufacturer.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Manufacturer] WHERE [ID] = {manufacturer.Id}") == 0);
        }

    }
}
