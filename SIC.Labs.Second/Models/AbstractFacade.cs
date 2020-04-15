using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Models
{
    public abstract class AbstractFacade<T>
    {
        public IRepository<T> Repository { get; set; }

        public IWriter<T> Writer { get; set; }

        public IReader<T> Reader { get; set; }


        public AbstractFacade(IRepository<T> repository)
        {
            Repository = repository;
        }

        public AbstractFacade(IRepository<T> repository, IWriter<T> writer, IReader<T> reader) : this(repository)
        {
            Writer = writer;
            Reader = reader;
        }


        public abstract void CreateItem(object sender, EventArgs e);

        public abstract void ReadItem(object sender, EventArgs e);

        public abstract void UpdateItem(object sender, EventArgs e);

        public abstract void DeleteItem(object sender, EventArgs e);

        public abstract void ReadCollection(object sender, EventArgs e);

        public abstract void ReadFromFile(object sender, EventArgs e);

        public abstract void WriteInFile(object sender, EventArgs e);

    }
}
