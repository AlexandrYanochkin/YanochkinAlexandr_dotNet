using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class StockItemRepository : BaseRepository, IRepository<StockItem>
    {
        public StockItemRepository(string connectionString) : base(connectionString)
        {
        }

        public void Create(StockItem item)
            => ExecuteNonQuery($"INSERT INTO [StockItem] VALUES " +
                $"({item.Count},{item.StockId},{item.CommodityId})");

        public void Delete(int id)
            => ExecuteNonQuery($"DELETE FROM [StockItem] WHERE [ID] = {id}");

        public IEnumerable<StockItem> GetCollection()
        {
            return ReadItems($"SELECT * FROM [StockItem]", sqlReader => new StockItem
            {
                Id = sqlReader.GetInt32(0),
                Count = sqlReader.GetInt32(1),
                StockId = sqlReader.GetInt32(2),
                CommodityId = sqlReader.GetInt32(3)
            });
        }

        public StockItem Read(int id)
        {
            return ReadItem($"SELECT * FROM [StockItem] WHERE [ID] = {id}", sqlReader => new StockItem
            {
                Id = sqlReader.GetInt32(0),
                Count = sqlReader.GetInt32(1),
                StockId = sqlReader.GetInt32(2),
                CommodityId = sqlReader.GetInt32(3)
            });
        }

        public void Update(StockItem item)
            => ExecuteNonQuery($"UPDATE [StockItem] SET " +
                $"[Count] = {item.Count}," +
                $"[StockID] = {item.StockId}," +
                $"[CommodityID] = {item.CommodityId}," +
                $"WHERE [ID] = {item.Id}");
    }
}
