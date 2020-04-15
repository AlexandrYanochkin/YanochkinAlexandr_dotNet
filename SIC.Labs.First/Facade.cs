using CsvHelper.TypeConversion;
using NLog;
using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SIC.Labs.First
{
    public class Facade
    {
        public ILogger Logger { get; set; }

        public IWriter<Student> Writer { get; set; }

        public IReader<Student> Reader { get; set; }

        public string InputPath { get; set; }

        public string OutputPath { get; set; }

        public Facade(IReader<Student> reader, IWriter<Student> writer, ILogger logger,string inputPath,string outputPath) 
        {
            Reader = reader;
            Writer = writer;
            Logger = logger;
            InputPath = inputPath;
            OutputPath = outputPath;
        }

        public void Process()
        {
            try
            {
                var students = Reader.Read(InputPath).ToList();

                string subjectsStr = students.First().Grades.Select(t => t.Subject).Aggregate((fVal, sVal) => $"{fVal};{sVal}");

                Console.WriteLine($"Surname;Name;Patronymic;{subjectsStr}");

                students.ForEach(stdnt => Console.WriteLine(stdnt));

                Writer.Write(OutputPath, students);
            }
            catch (FormatException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (FileNotFoundException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, e.Message);
            }
            catch(InvalidOperationException e)
            {
                Logger.Error(e, e.Message);
            }
            catch(TypeConverterException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (CsvHelper.MissingFieldException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }

        }

    }
}
