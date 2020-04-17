using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.Connections;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Writers.JsonWriters;
using SIC.Labs.Second.Components.Services.Writers.XmlWriters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace SIC.Labs.Second
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine(SQLConnector.ConnectionString);

            Order order = new Order
            {
                CreationDate = DateTime.Parse("05-10-2015"),
                ModificationDate = DateTime.Parse("09-25-2015"),
                Count = 45,
                FullPrice = 45.5M,
                Status = OrderStatus.SentToClient,
                EmployeeId = 1,
                StockItemId = 1
            };

            Console.WriteLine(order.Equals(order));

            //  new MainMenu() { DataAccess = DAOFactory.GetFactory(TypeOfFactory.MSSQL) }.Menu();
        }
    }
}

