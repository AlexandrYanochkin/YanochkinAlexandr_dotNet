using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace SIC.Labs.Second.Components.DAL.Connections
{
    public static class SQLConnector
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;

    }
}
