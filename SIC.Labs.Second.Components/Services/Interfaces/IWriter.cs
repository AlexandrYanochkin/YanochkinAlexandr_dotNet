using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Interfaces
{
    public interface IWriter<T>
    {
        void Write(string path, IEnumerable<T> collection);
    }
}
