using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T item);
        T Read(int id);
        IEnumerable<T> GetCollection();
        void Update(T item);
        void Delete(int id);
    }
}
