using NLog;
using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using SIC.Labs.First.Services.Readers;
using SIC.Labs.First.Services.Writers;
using System;
using System.IO;
using System.Linq;
using CommandLine;
using SIC.Labs.First.Models.Factory;
using SIC.Labs.First.Models;

namespace SIC.Labs.First
{

    class Program
    {
        private static IReader<Student> Reader = new ReaderCsv();
        
        private static IWriter<Student> Writer;

        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            
            try
            {
                CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(item =>
                {
                    Writer = GetFactory(item.Format).FactoryMethod();

                    Facade facade = new Facade(Reader, Writer,
                        Logger, item.InputFile, $"{item.OutputFile}.{item.Format}");

                    facade.Process();

                });
            }
            catch(FormatException e)
            {
                Logger.Error(e, e.Message);
            }

           
            
            Console.ReadKey();
        }

        public static IFactory<IWriter<Student>> GetFactory(string format)
        {
            IFactory<IWriter<Student>> factory;

            format = string.IsNullOrEmpty(format) ? format : format.ToLower();

            switch (format)
            {
                case "json":
                    factory = new JsonFactory();
                    break;


                case "xlsx":
                    factory = new ExcelFactory();
                    break;

                default:
                    throw new FormatException("Incorrect format!");

            }

            return factory;
        }

    }
}


