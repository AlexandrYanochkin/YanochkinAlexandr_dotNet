using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Second.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Services.ConsoleInputOfItems
{
    public class InputOfEmployee : IInputOfItem<Employee>
    {
        public void InputItem(out Employee item)
        {
            Employee employee = new Employee();

            Console.WriteLine("Input employee's FullName:");
            employee.FullName = Console.ReadLine();
            Console.WriteLine("Input employee's Age:");
            employee.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("Input employee's PhoneNumber:");
            employee.PhoneNumber = Console.ReadLine();

            employee.Validate();
            item = employee;
        }
    }
}
