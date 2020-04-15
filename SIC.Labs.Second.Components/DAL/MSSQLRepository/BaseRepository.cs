using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.DAL.MSSQLRepository
{
    public class BaseRepository : IDisposable
    {
        protected SqlConnection sqlConnection;


        public BaseRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }



        protected void ExecuteNonQuery(string command)
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        private SqlDataReader GetSqlReader(string command)
        {
            SqlDataReader sqlDataReader = null;

            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                sqlDataReader = sqlCommand.ExecuteReader();


            return sqlDataReader;
        }

        protected IEnumerable<T> ReadItems<T>(string command, Func<SqlDataReader, T> readFunc)
        {           
            List<T> list = new List<T>();

            using (SqlDataReader sqlDataReader = GetSqlReader(command))
            {
                while (sqlDataReader.Read())
                    list.Add(readFunc(sqlDataReader));

                sqlConnection.Close();
            }


            return list;
        }

        protected T ReadItem<T>(string command, Func<SqlDataReader, T> readItem)
        {
            T item = default(T);

            using (SqlDataReader sqlDataReader = GetSqlReader(command))
            {
                if (sqlDataReader.Read())
                    item = readItem(sqlDataReader);

                sqlConnection.Close();
            }


            return item;
        }


        protected async void ExecuteNonQueryAsync(string command)
        {
            await Task.Run(() => ExecuteNonQuery(command));
        }

        protected async Task<T> ReadItemAsync<T>(string command, Func<SqlDataReader,T> readItem)
        {
            return await Task.Run(() => ReadItem(command, readItem));
        }

        protected IEnumerable<T> ReadItemsAsync<T>(string command, Func<SqlDataReader, T> readFunc)
        {
            return Task.Run(() => ReadItems(command, readFunc)).Result;
        }




        public void Dispose()
            => sqlConnection.Dispose();
    }
}
