using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class StockRepository : BaseRepository, IRepository<Stock>
    {
        public StockRepository(string connectionString) : base(connectionString)
        {
        }

        public Task CreateAsync(Stock item)
            => ExecuteNonQueryAsync($"INSERT INTO [Stock] VALUES" +
                $" ('{item.Name}'," +
                $" '{item.Address}'," +
                $" {item.Allowance})");

        public Task DeleteAsync(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [Stock] WHERE [ID] = {id}");

        public Task<Stock> ReadAsync(int id)
        {
            return ReadItemAsync($"SELECT * FROM [Stock] WHERE [ID] = {id}",sqlReader => new Stock
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Address = sqlReader.GetString(2),
                Allowance = sqlReader.GetDouble(3)
            });
        }

        public Task UpdateAsync(Stock item)
         => ExecuteNonQueryAsync($"UPDATE [Stock] SET" +
                $" [Name] = '{item.Name}'," +
                $" [Address] = '{item.Address}'," +
                $" [Allowance] = '{item.Allowance}'" +
                $"WHERE [ID] = {item.Id}");

        public Task<IEnumerable<Stock>> GetCollectionAsync()
        {
            return ReadItemsAsync($"SELECT * FROM [Stock]", sqlReader => new Stock
            { 
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Address = sqlReader.GetString(2),
                Allowance = sqlReader.GetDouble(3)
            });
        }

    }
}
