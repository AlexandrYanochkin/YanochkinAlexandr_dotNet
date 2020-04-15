using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class OrderRepository : BaseRepository, IRepository<Order>
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public void Create(Order item)
            => ExecuteNonQuery($"INSERT INTO [Order] VALUES " +
                $"('{item.CreationDate.ToString("MM-dd-yyyy")}','{item.ModificationDate.ToString("MM-dd-yyyy")}',{(int)item.Status}," +
                $"{item.Count},{item.FullPrice},{item.StockItemId},{item.EmployeeId})");

        public void Delete(int id)
            => ExecuteNonQuery($"DELETE FROM [Order] WHERE [ID] = {id}");

        public IEnumerable<Order> GetCollection()
        {
            return ReadItems($"SELECT * FROM [Order]", sqlReader => new Order
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

        public Order Read(int id)
        {
            return ReadItem($"SELECT * FROM [Order] WHERE [ID] = {id}", sqlReader => new Order
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

        public void Update(Order item)
            => ExecuteNonQuery($"UPDATE [Order] SET " +
                $"[CreationDate] = '{item.CreationDate.ToString("MM-dd-yyyy")}'," +
                $"[ModificationDate] = '{item.ModificationDate.ToString("MM-dd-yyyy")}'," +
                $"[Status] = {(int)item.Status}," +
                $"[Count] = {item.Count}," +
                $"[FullPrice] = {item.FullPrice}," +
                $"[StockItemID] = {item.StockItemId}," +
                $"[EmployeeID] = {item.EmployeeId}" +
                $"WHERE [ID] = {item.Id}");
    }
}
