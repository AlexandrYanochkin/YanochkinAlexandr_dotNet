using SIC.Labs.First.Models.Exceptions;
using SIC.Labs.First.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Services.Validators
{
    public static class GradeValidator
    {
        public static void Validate(this Grade grade)
        {
            if (grade == null)
                throw new GradeException("Grade has null value");
            else if (grade.Value < 0 || grade.Value > 10)
                throw new GradeException("Grade can't be less than 0 and more than 10");
            else if (grade.Subject == null)
                throw new GradeException("Incorrect subject's value");
        }

    }
}
