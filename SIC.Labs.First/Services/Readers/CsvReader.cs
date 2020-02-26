using SIC.Labs.First.Services.Validators;
using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;

namespace SIC.Labs.First.Services.Readers
{
    public class ReaderCsv : IReader<Student>
    {
        public IEnumerable<Student> Read(string path)
        {
            path.ValidateCsvPath();

            path.CheckCsvFileExist();

            List<Student> students = new List<Student>();

            List<string> subjects = new List<string>();

            using (StreamReader streamReader = new StreamReader(path))
            using (CsvReader csvReader = new CsvReader(streamReader,CultureInfo.InvariantCulture))
            {
                csvReader.Read();

                int ind = 0;
                string headerStr;

                while (csvReader.TryGetField<string>(ind++, out headerStr))
                    subjects.Add(headerStr);

                subjects = subjects.Skip(3).ToList();


                while (csvReader.Read())
                {
                    Student student = new Student() { Grades = new List<Grade>() };
                    ind = 0;
                    int gradeValue = 0;
                    List<int> grades = new List<int>();

                    student.Surname = csvReader.GetField<string>(ind++);
                    student.Name = csvReader.GetField<string>(ind++);
                    student.Patronymic = csvReader.GetField<string>(ind++);

                    while (csvReader.TryGetField<int>(ind++, out gradeValue))
                        grades.Add(gradeValue);

                    if (grades.Count != subjects.Count)
                    {
                        throw new InvalidOperationException("There was an incorrect file!");
                    }
                    else
                        student.Grades.AddRange(grades.Zip(subjects, (gradeVal, subjVal) => new Grade
                        {
                            Subject = subjVal,
                            Value = gradeVal
                        }));

                     students.Add(student);
                }

            }

            students.ForEach(stdnt => stdnt.Validate());

            return students;
        }

    }
}
