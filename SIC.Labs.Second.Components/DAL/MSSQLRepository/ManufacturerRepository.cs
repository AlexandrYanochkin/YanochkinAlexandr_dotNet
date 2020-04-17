using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class ManufacturerRepository : BaseRepository,IRepository<Manufacturer>
    {
        public ManufacturerRepository(string connectionString) : base(connectionString)
        { 
        }

        public Task CreateAsync(Manufacturer item)
            => ExecuteNonQueryAsync($"INSERT INTO [Manufacturer] VALUES " +
                $"('{item.Name}','{item.Address}','{item.PhoneNumber}')");

        public Task DeleteAsync(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [Manufacturer] WHERE [ID] = {id}");

        public Task<Manufacturer> ReadAsync(int id)
        {
            return ReadItemAsync($"SELECT * FROM [Manufacturer] WHERE [ID] = {id}", sqlDataReader => new Manufacturer
            {
                Id = sqlDataReader.GetInt32(0),
                Name = sqlDataReader.GetString(1),
                Address = sqlDataReader.GetString(2),
                PhoneNumber = sqlDataReader.GetString(3)
            });
        }

        public Task UpdateAsync(Manufacturer item)
            => ExecuteNonQueryAsync($"UPDATE [Manufacturer] SET " +
                $"[Name] = N'{item.Name}'," +
                $"[Address] = N'{item.Address}'," +
                $"[PhoneNumber] = N'{item.PhoneNumber}' " +
                $"WHERE [ID] = {item.Id}");

        public Task<IEnumerable<Manufacturer>> GetCollectionAsync()
        {
            return ReadItemsAsync("SELECT * FROM [Manufacturer]", sqlDataReader => new Manufacturer
            {
                Id = sqlDataReader.GetInt32(0),
                Name = sqlDataReader.GetString(1),
                Address = sqlDataReader.GetString(2),
                PhoneNumber = sqlDataReader.GetString(3)
            });
        }
    }
}
