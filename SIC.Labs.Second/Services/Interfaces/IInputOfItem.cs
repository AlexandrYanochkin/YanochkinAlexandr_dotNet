using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.Interfaces
{
    public interface IInputOfItem<T> 
    {
        void InputItem(out T item);
    }
}
