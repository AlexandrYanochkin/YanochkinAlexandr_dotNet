using System.Data.SqlClient;

namespace SIC.Labs.Second.Components.Tests.Services.Interfaces
{
    public interface IDataBaseWorker
    {
        void ExecuteNonQuery(string command);
        T ExecuteScalar<T>(string command);  
    }
}
