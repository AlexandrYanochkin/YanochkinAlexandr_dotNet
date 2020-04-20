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

            //new MainMenu() { DataAccess = DAOFactory.GetFactory(TypeOfFactory.MSSQL) }.Menu();
        }
    }
}

