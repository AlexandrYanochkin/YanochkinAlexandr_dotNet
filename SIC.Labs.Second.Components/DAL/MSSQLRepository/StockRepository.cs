using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Collections.Generic;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class StockRepository : BaseRepository, IRepository<Stock>
    {
        public StockRepository(string connectionString) : base(connectionString)
        {
        }


        public void Create(Stock item)
            => ExecuteNonQuery($"INSERT INTO [Stocks] VALUES" +
                $" ({item.Name},{item.Address},{item.Allowance})");

        public void Delete(int id)
            => ExecuteNonQuery($"DELETE FROM [Stocks] WHERE [ID] = {id}");

        public Stock Read(int id)
        {
            return ReadItem($"SELECT * FROM [Stocks] WHERE [ID] = {id}",sqlReader => new Stock
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Address = sqlReader.GetString(2),
                Allowance = sqlReader.GetDouble(3)
            });
        }

        public void Update(Stock item)
         => ExecuteNonQuery($"UPDATE [Stocks] SET" +
                $" [Name] = '{item.Name}'," +
                $" [Address] = '{item.Address}'," +
                $" [Allowance] = '{item.Allowance}'" +
                $"WHERE [ID] = {item.Id}");

        public IEnumerable<Stock> GetCollection()
        {
            return ReadItems($"SELECT * FROM [Stocks]", sqlReader => new Stock
            { 
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Address = sqlReader.GetString(2),
                Allowance = sqlReader.GetDouble(3)
            });
        }

    }
}
