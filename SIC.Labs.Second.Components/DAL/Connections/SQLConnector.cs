using System.Configuration;

namespace SIC.Labs.Second.Components.DAL.Connections
{
    public static class SQLConnector
    {
        public static readonly string ConnectionString = @"Data Source=(localdb)\
                    MSSQLLocalDB;Initial Catalog = StockDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;
            TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;
    }
}
