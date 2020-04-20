using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Factory;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Readers;
using SIC.Labs.Second.Components.Services.Writers.JsonWriters;
using SIC.Labs.Second.Components.Services.Writers.XmlWriters;
using SIC.Labs.Second.Models;
using SIC.Labs.Second.Models.Facades.ConsoleFacades;
using SIC.Labs.Second.Services.ConsoleInputOfItems;
using SIC.Labs.Second.Services.Interfaces;
using System;


namespace SIC.Labs.Second
{
    public class MainMenu
    {
        public DAO DataAccess { get; set; }

        public FileFormat FileFormat { get; set; }

        public void Menu()
        {
            bool exit = false;

            while (!exit)
            {

                Console.WriteLine($"\n\t\t\tStockDB Menu" +
                    $"\n\t1.Employee Menu" +
                    $"\n\t2.Manufacturer Menu" +
                    $"\n\t3.Commodity Menu" +
                    $"\n\t4.StockItem Menu" +
                    $"\n\t5.Stock Menu" +
                    $"\n\t6.Order Menu" +
                    $"\n\t7.Choose FileFormat" +
                    $"\n\t9.Exit");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                Console.Clear();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        GetEntityMenu(GetFacade(DataAccess.Employees, new InputOfEmployee())).Menu();
                        break;

                    case ConsoleKey.D2:
                        GetEntityMenu(GetFacade(DataAccess.Manufacturers, new InputOfManufacturer())).Menu();
                        break;

                    case ConsoleKey.D3:
                        GetEntityMenu(GetFacade(DataAccess.Commodities, new InputOfCommodity())).Menu();
                        break;

                    case ConsoleKey.D4:
                        GetEntityMenu(GetFacade(DataAccess.StockItems, new InputOfStockItem())).Menu();
                        break;

                    case ConsoleKey.D5:
                        GetEntityMenu(GetFacade(DataAccess.Stocks, new InputOfStock())).Menu();
                        break;

                    case ConsoleKey.D6:
                        GetEntityMenu(GetFacade(DataAccess.Orders, new InputOfOrder())).Menu();
                        break;

                    case ConsoleKey.D7:
                        ChooseFormatOfFile();
                        break;

                    case ConsoleKey.D9:
                        exit = true;
                        break;
                }

                //Console.ReadKey();
                //Console.Clear();
            }
        }

        private void ChooseFormatOfFile()
        {
            Console.WriteLine("Choose Format:" +
                "\n\t1.Xml" +
                "\n\t2.Json");

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    FileFormat = FileFormat.Xml;
                    break;

                case ConsoleKey.D2:
                    FileFormat = FileFormat.Json;
                    break;
            }

            Console.Clear();
        }

        private EntityMenu<T> GetEntityMenu<T>(AbstractFacade<T> abstractFacade)
            => new EntityMenu<T>(abstractFacade);

        private ConsoleFacade<T> GetFacade<T>(IRepository<T> repository, IInputOfItem<T> inputOfItem)
            => new ConsoleFacade<T>(repository, GetWriter<T>(FileFormat), GetReader<T>(FileFormat))
            {
                PathToFile = $@"..\..\..\Files\{FileFormat}\ArrayOf{typeof(T).Name}.{FileFormat.ToString().ToLower()}",
                InputOfItem = inputOfItem
            };

        private IWriter<T> GetWriter<T>(FileFormat fileFormat)
        {
            IWriter<T> writer = null;

            switch (fileFormat)
            {
                case FileFormat.Xml:
                    writer = new XmlSerializer<T>();
                    break;

                case FileFormat.Json:
                    writer = new JsonSerializer<T>();
                    break;

                default:
                    throw new ArgumentException("Invalida Argument!!!");
            }

            return writer;
        }

        private IReader<T> GetReader<T>(FileFormat fileFormat)
        {
            IReader<T> reader = null;

            switch (fileFormat)
            {
                case FileFormat.Xml:
                    reader = new XmlDeserializer<T>();
                    break;

                case FileFormat.Json:
                    reader = new JsonDeserializer<T>();
                    break;

                default:
                    throw new ArgumentException("Invalida Argument!!!");
            }

            return reader;
        }

    }
}
