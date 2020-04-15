using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Interfaces
{
    public interface IFactory<T>
    {
        T FactoryMethod();
    }
}
