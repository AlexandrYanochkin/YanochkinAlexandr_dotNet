using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Services.Interfaces;

namespace SIC.Labs.Second.Models.Facades.ConsoleFacades
{
    public  class ConsoleFacade<T> : AbstractFacade<T>
    {
        public string PathToFile { get; set; }

        public IInputOfItem<T> InputOfItem { get; set; }

        


        public ConsoleFacade(IRepository<T> repository) : base(repository)
        {
        }

        public ConsoleFacade(IRepository<T> repository, IWriter<T> writer, IReader<T> reader) : base(repository, writer, reader)
        {
        }

        public ConsoleFacade(IRepository<T> repository, IWriter<T> writer, IReader<T> reader, IInputOfItem<T> inputOfItem) 
            : this(repository, writer, reader)
        {
            InputOfItem = inputOfItem;
        }



        public override void CreateItem(object sender, EventArgs e)
        {
            try
            {
                InputOfItem.InputItem(out T item);

                Repository.Create(item);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StockException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void ReadItem(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine($"Input Id of {typeof(T).Name}:");
                int id = int.Parse(Console.ReadLine());

                var item = Repository.Read(id);

                Console.WriteLine(item);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateItem(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("\nInput Id of Item:");
                int id = int.Parse(Console.ReadLine());

                var item = Repository.Read(id);

                InputOfItem.InputItem(out item);

                Repository.Update(item);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StockException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void DeleteItem(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("\nInput Id for Delete:");
                int id = int.Parse(Console.ReadLine());

                Repository.Delete(id);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine("\nInput Id for Delete:");
            //if (int.TryParse(Console.ReadLine(), out int id))
            //{
            //    Repository.Delete(id);
            //}
        }

        public override void ReadCollection(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine();
                var collection = Repository.GetCollection().ToList();
                collection.ForEach(item => Console.WriteLine(item));
                Console.WriteLine();
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }

        public override void ReadFromFile(object sender, EventArgs e)
        {
            try
            {
                var collection = Reader.Read(PathToFile);

                foreach (var item in collection)
                    Repository.Create(item);

                Console.WriteLine("Done!!!");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public override void WriteInFile(object sender, EventArgs e)
        {
            try
            {
                var collection = Repository.GetCollection();

                Writer.Write(PathToFile, collection);

                Console.WriteLine("Done!!!");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
