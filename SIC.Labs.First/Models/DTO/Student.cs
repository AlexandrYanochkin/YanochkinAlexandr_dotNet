using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIC.Labs.First.Models.DTO
{
    public class Student
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public List<Grade> Grades { get; set; }


        public override string ToString()
            => ($"{Surname};{Name};{Patronymic};" +
            $"{Grades.Select(t => t.Value.ToString()).Aggregate((fVal,sVal) => $"{fVal};{sVal}")}");
    }
}
