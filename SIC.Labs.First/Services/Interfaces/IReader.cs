using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Services.Interfaces
{
    public interface IReader<T>
    {
        IEnumerable<T> Read(string path);
    }
}
