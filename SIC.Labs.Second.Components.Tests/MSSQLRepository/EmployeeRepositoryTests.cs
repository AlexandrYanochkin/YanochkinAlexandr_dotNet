using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Tests.DataBaseWorkers;
using System.Configuration;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.Tests.MSSQLRepository
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        public DAO DataAccess { get; set; } = DAOFactory.GetFactory(TypeOfFactory.MSSQL);

        public SQLWorker SqlWorker { get; set; } = new SQLWorker(SQLConnector.ConnectionString);

        [TestMethod]
        public async Task CreateAsync_ShouldCreateEmployee_True()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };


            //act
            await DataAccess.Employees.CreateAsync(employee);
            employee.Id = SqlWorker.ExecuteScalar<int>($"SELECT MAX([ID]) FROM [Employee]");
            Employee employeeForCompare = DataAccess.Employees.ReadAsync(employee.Id).Result;
            await DataAccess.Employees.DeleteAsync(employee.Id);


            //assert
            Assert.IsTrue(employee.Equals(employeeForCompare));
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReadEmployee_True()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };


            //act
            await DataAccess.Employees.CreateAsync(employee);
            employee.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Employee]");       
            Employee employeeForCompare = DataAccess.Employees.ReadAsync(employee.Id).Result;
            await DataAccess.Employees.DeleteAsync(employee.Id);


            //assert
            Assert.IsTrue(employee.Equals(employeeForCompare));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateEmployee_True()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };

            //act
            await DataAccess.Employees.CreateAsync(employee);
            employee.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Employee]");
            employee.FullName += "Updated";
            await DataAccess.Employees.UpdateAsync(employee);
            Employee employeeForCompare = DataAccess.Employees.ReadAsync(employee.Id).Result;
            await DataAccess.Employees.DeleteAsync(employee.Id);

            //assert
            Assert.IsTrue(employeeForCompare.Equals(employee));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteEmployee_True()
        {
            //arrange
            Employee employee = new Employee
            {
                FullName = "TestFullName",
                Age = 25,
                PhoneNumber = "TestPhoneNumber"
            };


            //act
            await DataAccess.Employees.CreateAsync(employee);
            employee.Id = SqlWorker.ExecuteScalar<int>("SELECT MAX([ID]) FROM [Employee]");
            Employee employeeForCompare = DataAccess.Employees.ReadAsync(employee.Id).Result;
            await DataAccess.Employees.DeleteAsync(employee.Id);


            //assert
            Assert.IsTrue(employeeForCompare.Equals(employee));    
        }

    }
}
