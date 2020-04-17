using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Tests.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SIC.Labs.Second.Components.Tests.DataBaseWorkers
{
    public class SQLWorker : IDataBaseWorker, IDisposable
    {
        protected SqlConnection sqlConnection;

        public SQLWorker(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void ExecuteNonQuery(string command)
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public T ExecuteScalar<T>(string command)
        {
            T item = default(T);

            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                item = (T)sqlCommand.ExecuteScalar();

            sqlConnection.Close();

            return item;
        }

        public void Dispose()
            => sqlConnection.Dispose();
    }
}
