using SIC.Labs.Second.Components.Models.DTO;
using System.Collections.Generic;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class EmployeeRepository : BaseRepository, IRepository<Employee>
    {

        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }


        public void Create(Employee item)
             => ExecuteNonQueryAsync($"INSERT INTO [Employee] VALUES " +
                 $"(N'{item.FullName}'," +
                 $"{item.Age}," +
                 $"N'{item.PhoneNumber}')");

        public void Delete(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [Employee] WHERE [ID] = {id}");

        public Employee Read(int id)
        {
            return ReadItemAsync($"SELECT * FROM [Employee] WHERE [ID] = {id}", sqlReader => new Employee
            {
                Id = sqlReader.GetInt32(0),
                FullName = sqlReader.GetString(1),
                Age = sqlReader.GetInt32(2),
                PhoneNumber = sqlReader.GetString(3)
            }).Result;
        }

        public void Update(Employee item)
            => ExecuteNonQueryAsync($"UPDATE [Employee] SET " +
                   $"[FullName] = N'{item.FullName}'," +
                   $"[Age] = {item.Age}," +
                   $"[PhoneNumber] = N'{item.PhoneNumber}'" +
                   $" WHERE [ID] = {item.Id}");

        public IEnumerable<Employee> GetCollection()
        {
            return ReadItemsAsync($"SELECT * FROM [Employee]", sqlReader => new Employee() 
            {
                Id = sqlReader.GetInt32(0),
                FullName = sqlReader.GetString(1),
                Age = sqlReader.GetInt32(2),
                PhoneNumber = sqlReader.GetString(3)
            });
        }


    }
}

