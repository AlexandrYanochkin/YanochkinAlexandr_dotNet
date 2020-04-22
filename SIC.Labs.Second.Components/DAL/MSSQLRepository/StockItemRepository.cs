using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class StockItemRepository : BaseRepository, IRepository<StockItem>
    {
        public StockItemRepository(string connectionString) : base(connectionString)
        {
        }

        public Task CreateAsync(StockItem item)
            => ExecuteNonQueryAsync($"INSERT INTO [StockItem] VALUES " +
                $"({item.Count},{item.StockId},{item.CommodityId})");

        public Task DeleteAsync(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [StockItem] WHERE [ID] = {id}");

        public Task<IEnumerable<StockItem>> GetCollectionAsync()
        {
            return ReadItemsAsync($"SELECT * FROM [StockItem]", sqlReader => new StockItem
            {
                Id = sqlReader.GetInt32(0),
                Count = sqlReader.GetInt32(1),
                StockId = sqlReader.GetInt32(2),
                CommodityId = sqlReader.GetInt32(3)
            });
        }

        public Task<StockItem> ReadAsync(int id)
        {
            return ReadItemAsync($"SELECT * FROM [StockItem] WHERE [ID] = {id}", sqlReader => new StockItem
            {
                Id = sqlReader.GetInt32(0),
                Count = sqlReader.GetInt32(1),
                StockId = sqlReader.GetInt32(2),
                CommodityId = sqlReader.GetInt32(3)
            });
        }

        public Task UpdateAsync(StockItem item)
            => ExecuteNonQueryAsync($"UPDATE [StockItem] SET " +
                $"[Count] = {item.Count}," +
                $"[StockID] = {item.StockId}," +
                $"[CommodityID] = {item.CommodityId}" +
                $"WHERE [ID] = {item.Id}");
    }
}
