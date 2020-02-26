using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SIC.Labs.First.Services.Validators;
using System.IO;

namespace SIC.Labs.First.Services.Writers
{
    public class WriterJson : IWriter<Student>
    {

        public void Write(string path, IEnumerable<Student> collection)
        {
            path.ValidateJsonPath();

            var resultsOfStudents = collection.Select(stdnt => new StudentResult()
            {
                Surname = stdnt.Surname,
                Name = stdnt.Name,
                Patronymic = stdnt.Patronymic,
                AverageGrade = stdnt.Grades.Average(t => t.Value)
            });

            string jsonResults = JsonConvert.SerializeObject(resultsOfStudents,Formatting.Indented);

            using (StreamWriter streamWriter = new StreamWriter(path))       
                streamWriter.Write(jsonResults);
                     
        }

    }
}
