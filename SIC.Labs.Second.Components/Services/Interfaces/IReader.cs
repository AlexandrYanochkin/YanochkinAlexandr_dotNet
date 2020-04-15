using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Interfaces
{
    public interface IReader<T>
    {
        IEnumerable<T> Read(string path);
    }
}
