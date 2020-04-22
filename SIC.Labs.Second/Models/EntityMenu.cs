using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Models
{
    public class EntityMenu<T>
    {
        public EntityMenu()
        {
        }

        public EntityMenu(AbstractFacade<T> facade)
        {
            OnCreate += facade.CreateItem;
            OnRead += facade.ReadItem;
            OnUpdate += facade.UpdateItem;
            OnDelete += facade.DeleteItem;
            OnReadCollection += facade.ReadCollection;
            OnReadFromFile += facade.ReadFromFile;
            OnWriteInFile += facade.WriteInFile;
        }

        public void Menu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine($"\n\t\t\t {typeof(T).Name} Menu" +
                                  $"\n\t1.Create {typeof(T).Name}" +
                                  $"\n\t2.Read {typeof(T).Name}" +
                                  $"\n\t3.Update {typeof(T).Name}" +
                                  $"\n\t4.Delete {typeof(T).Name}" +
                                  $"\n\t5.GetCollection of {typeof(T).Name}" +
                                  $"\n\t6.ReadFromFile" +
                                  $"\n\t7.WriteInFile" +
                                  $"\n\tanother.Exit\n");

                ConsoleKeyInfo consoleKey = Console.ReadKey();

                Console.WriteLine();

                switch (consoleKey.Key)
                {
                    case ConsoleKey.D1:
                        OnCreate?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D2:
                        OnRead?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D3:
                        OnUpdate?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D4:
                        OnDelete?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D5:
                        OnReadCollection?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D6:
                        OnReadFromFile?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D7:
                        OnWriteInFile?.Invoke(this, EventArgs.Empty);
                        break;

                    default:
                        exit = true;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }


        public event EventHandler OnCreate;

        public event EventHandler OnRead;

        public event EventHandler OnUpdate;

        public event EventHandler OnDelete;

        public event EventHandler OnReadCollection;

        public event EventHandler OnReadFromFile;

        public event EventHandler OnWriteInFile;

    }
}
