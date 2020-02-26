using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Models.Exceptions;
using SIC.Labs.First.Services.Validators;

namespace SIC.Labs.First.Services.Validators
{
    public static class StudentValidator
    {
        public static void Validate(this Student student)
        {
            if (student == null)
                throw new StudentException("Student object has null value!!!");
            else if (student.Grades == null)
                throw new StudentException("Grades can't be null!!!");
 
                ValidateValue(student.Name, "Incorrect student's name!!!");
                ValidateValue(student.Surname, "Incorrect student's surname!!!");
                ValidateValue(student.Patronymic, "Incorrect student's patronymic!!!");


                student.Grades.ForEach(grd => grd.Validate());

                var grds = student.Grades;

                if (grds.Any(fGrd => grds.Count(sGrd => sGrd.Subject == fGrd.Subject) > 1))
                    throw new StudentException("There's a subject which repeats in collection!!!");
        }

        static void ValidateValue(string value,string outMessage)
        {
            if (value == null)
                throw new StudentException(outMessage);
        }

    }
}
