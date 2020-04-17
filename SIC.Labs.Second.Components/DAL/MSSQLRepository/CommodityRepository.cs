using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class CommodityRepository : BaseRepository, IRepository<Commodity>
    {
        public CommodityRepository(string connectionString) : base(connectionString)
        {
        }

        public Task CreateAsync(Commodity item)
            => ExecuteNonQueryAsync($"INSERT INTO [Commodity] VALUES " +
                $"('{item.Name}'," +
                $"'{item.Price}'," +
                $"{item.ManufacturerId})");

        public Task DeleteAsync(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [Commodity] WHERE [ID] = {id}");

        public Task<Commodity> ReadAsync(int id)
        {
            return ReadItemAsync($"SELECT * FROM [Commodity] WHERE [ID] = {id}", sqlReader => new Commodity
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Price = sqlReader.GetDecimal(2),
                ManufacturerId = sqlReader.GetInt32(3)
            });
        }

        public Task UpdateAsync(Commodity item)
           => ExecuteNonQueryAsync($"UPDATE [Commodity] SET " +
               $"[Name] = '{item.Name}'," +
               $"[Price] = {item.Price}," +
               $"[ManufacturerID] = {item.ManufacturerId}" +
               $" WHERE [ID] = {item.Id}");

        public Task<IEnumerable<Commodity>> GetCollectionAsync()
        {
            return ReadItemsAsync($"SELECT * FROM [Commodity]", sqlReader => new Commodity
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Price = sqlReader.GetDecimal(2),
                ManufacturerId = sqlReader.GetInt32(3)
            });

        }

    }
}
