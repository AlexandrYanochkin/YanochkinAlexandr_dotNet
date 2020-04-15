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


        public void Create(Commodity item)
            => ExecuteNonQuery($"INSERT INTO [Commodity] VALUES " +
                $"('{item.Name}'," +
                $"'{item.Price}'," +
                $"{item.ManufacturerId})");

        public void Delete(int id)
            => ExecuteNonQuery($"DELETE FROM [Commodity] WHERE [ID] = {id}");

        public Commodity Read(int id)
        {
            return ReadItem($"SELECT * FROM [Commodity] WHERE [ID] = {id}", sqlReader => new Commodity
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Price = sqlReader.GetDecimal(2),
                ManufacturerId = sqlReader.GetInt32(3)
            });
        }

        public void Update(Commodity item)
           => ExecuteNonQuery($"UPDATE [Commodity] SET " +
               $"[Name] = '{item.Name}'," +
               $"[Price] = {item.Price}," +
               $"[ManufacturerID] = {item.ManufacturerId}" +
               $" WHERE [ID] = {item.Id}");

        public IEnumerable<Commodity> GetCollection()
        {
            return ReadItems($"SELECT * FROM [Commodity]", sqlReader => new Commodity
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1),
                Price = sqlReader.GetDecimal(2),
                ManufacturerId = sqlReader.GetInt32(3)
            });

        }

    }
}
