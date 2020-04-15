using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;
using System.Collections.Generic;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class ManufacturerRepository : BaseRepository,IRepository<Manufacturer>
    {

        public ManufacturerRepository(string connectionString) : base(connectionString)
        { 
        }


        public void Create(Manufacturer item)
            => ExecuteNonQuery($"INSERT INTO [Manufacturer] VALUES " +
                $"(N'{item.Name}',N'{item.Adress}',N'{item.PhoneNumber}')");

        public void Delete(int id)
            => ExecuteNonQuery($"DELETE FROM [Manufacturer] WHERE [ID] = {id}");

        public Manufacturer Read(int id)
        {
            return ReadItem($"SELECT * FROM [Manufacturer] WHERE [ID] = {id}", sqlDataReader => new Manufacturer
            {
                Id = sqlDataReader.GetInt32(0),
                Name = sqlDataReader.GetString(1),
                Adress = sqlDataReader.GetString(2),
                PhoneNumber = sqlDataReader.GetString(3)
            });
        }

        public void Update(Manufacturer item)
            => ExecuteNonQuery($"UPDATE [Manufacturer] SET " +
                $"[Name] = N'{item.Name}'," +
                $"[Adress] = N'{item.Adress}'," +
                $"[PhoneNumber] = N'{item.PhoneNumber}' " +
                $"WHERE [ID] = {item.Id}");

        public IEnumerable<Manufacturer> GetCollection()
        {
            return ReadItems("SELECT * FROM [Manufacturer]", sqlDataReader => new Manufacturer
            {
                Id = sqlDataReader.GetInt32(0),
                Name = sqlDataReader.GetString(1),
                Adress = sqlDataReader.GetString(2),
                PhoneNumber = sqlDataReader.GetString(3)
            });
        }

    }
}
