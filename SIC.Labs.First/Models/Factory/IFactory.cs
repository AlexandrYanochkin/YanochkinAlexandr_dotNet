using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Models.Factory
{
    public interface IFactory<T>
    {
        T FactoryMethod();
    }
}
