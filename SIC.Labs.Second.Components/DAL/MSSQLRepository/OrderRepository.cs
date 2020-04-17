using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class OrderRepository : BaseRepository, IRepository<Order>
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public Task CreateAsync(Order item)
            => ExecuteNonQueryAsync($"INSERT INTO [Order] VALUES " +
                $"('{item.CreationDate.ToString("MM-dd-yyyy")}'," +
                $"'{item.ModificationDate.ToString("MM-dd-yyyy")}'," +
                $"{(int)item.Status}," +
                $"{item.Count}," +
                $"{item.FullPrice}," +
                $"{item.StockItemId}," +
                $"{item.EmployeeId})");

        public Task DeleteAsync(int id)
            => ExecuteNonQueryAsync($"DELETE FROM [Order] WHERE [ID] = {id}");

        public Task<IEnumerable<Order>> GetCollectionAsync()
        {
            return ReadItemsAsync($"SELECT * FROM [Order]", sqlReader => new Order
            {
                Id = sqlReader.GetInt32(0),
                CreationDate = sqlReader.GetDateTime(1),
                ModificationDate = sqlReader.GetDateTime(2),
                Status = (OrderStatus)sqlReader.GetInt32(3),
                Count = sqlReader.GetInt32(4),
                FullPrice = sqlReader.GetDecimal(5),
                StockItemId = sqlReader.GetInt32(6),
                EmployeeId = sqlReader.GetInt32(7)
            });
        }

        public Task<Order> ReadAsync(int id)
        {
            return ReadItemAsync($"SELECT * FROM [Order] WHERE [ID] = {id}", sqlReader => new Order
            {
                Id = sqlReader.GetInt32(0),
                CreationDate = sqlReader.GetDateTime(1),
                ModificationDate = sqlReader.GetDateTime(2),
                Status = (OrderStatus)sqlReader.GetInt32(3),
                Count = sqlReader.GetInt32(4),
                FullPrice = sqlReader.GetDecimal(5),
                StockItemId = sqlReader.GetInt32(6),
                EmployeeId = sqlReader.GetInt32(7)
            });
        }

        public Task UpdateAsync(Order item)
            => ExecuteNonQueryAsync($"UPDATE [Order] SET " +
                $"[CreationDate] = '{item.CreationDate.ToString("MM-dd-yyyy")}'," +
                $"[ModificationDate] = '{item.ModificationDate.ToString("MM-dd-yyyy")}'," +
                $"[Status] = {(int)item.Status}," +
                $"[Count] = {item.Count}," +
                $"[FullPrice] = {item.FullPrice}," +
                $"[StockItemID] = {item.StockItemId}," +
                $"[EmployeeID] = {item.EmployeeId}" +
                $" WHERE [ID] = {item.Id}");
    }
}
