using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using System;

namespace SIC.Labs.Second.Components.Models.Factory
{
    public static class DAOFactory
    {
        public static DAO GetFactory(TypeOfFactory typeOfFactory)
        {
            switch (typeOfFactory)
            {
                case TypeOfFactory.MSSQL:
                    return new MSSQLFactory(SQLConnector.ConnectionString).FactoryMethod();


                default:
                    throw new NotImplementedException("Incorrect Enum Value!");            
            }
        }

    }
}
