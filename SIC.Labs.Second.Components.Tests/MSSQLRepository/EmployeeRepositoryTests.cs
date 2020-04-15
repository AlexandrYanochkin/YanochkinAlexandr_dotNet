using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.DAL.MSSQLRepository;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod()]
        public void CreateTest()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };
            bool result;

            //act
            DataAccess.Employees.Create(employee);

            employee.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Employee] WHERE " +
                $"[FullName] = '{employee.FullName}' AND " +
                $"[Age] = {employee.Age} AND " +
                $"[PhoneNumber] = '{employee.PhoneNumber}'");

            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Employee] WHERE [ID] = {employee.Id}") > 0);
            DataAccess.Employees.Delete(employee.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ReadTest()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };

            //act
            DataAccess.Employees.Create(employee);

            employee.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Employee] WHERE " +
                $"[FullName] = '{employee.FullName}' AND " +
                $"[Age] = {employee.Age} AND " +
                $"[PhoneNumber] = '{employee.PhoneNumber}'");

            Employee employeeForCompare = DataAccess.Employees.Read(employee.Id);
            DataAccess.Employees.Delete(employee.Id);

            //assert
            Assert.IsTrue(employee.Equals(employeeForCompare));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };
            bool result;

            //act
            DataAccess.Employees.Create(employee);

            employee.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Employee] WHERE " +
                $"[FullName] = '{employee.FullName}' AND " +
                $"[Age] = {employee.Age} AND " +
                $"[PhoneNumber] = '{employee.PhoneNumber}'");

            employee.FullName += "Updated";
            DataAccess.Employees.Update(employee);
            result = (SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Employee] WHERE [ID] = {employee.Id}") > 0);
            DataAccess.Employees.Delete(employee.Id);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };


            //act
            DataAccess.Employees.Create(employee);

            employee.Id = SqlWorker.ExecuteScalar<int>($"SELECT [ID] FROM [Employee] WHERE " +
                $"[FullName] = '{employee.FullName}' AND " +
                $"[Age] = {employee.Age} AND " +
                $"[PhoneNumber] = '{employee.PhoneNumber}'");

            DataAccess.Employees.Delete(employee.Id);

            //assert
            Assert.IsTrue(SqlWorker.ExecuteScalar<int>($"SELECT COUNT(*) FROM [Employee] WHERE [ID] = {employee.Id}") == 0);
        }

    }
}
