using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SIC.Labs.Second.Components.Services.Interfaces
{
    public interface IRepository<T>
    {
        Task CreateAsync(T item);
        Task<T> ReadAsync(int id);
        Task<IEnumerable<T>> GetCollectionAsync();
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
