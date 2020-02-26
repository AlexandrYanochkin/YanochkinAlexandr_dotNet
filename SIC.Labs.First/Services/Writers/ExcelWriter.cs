using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using SIC.Labs.First.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;

namespace SIC.Labs.First.Services.Writers
{
    public class ExcelWriter : IWriter<Student>
    {
        static ExcelPackage ExcelPackage = new ExcelPackage();

        static ExcelWorkbook WorkBook = ExcelPackage.Workbook;

        public void Write(string path, IEnumerable<Student> collection)
        {
            path.ValidateExcelPath();

            ExcelWorksheet worksheet = WorkBook.Worksheets.Add(path);

            var stdntsRslts = collection.Select(stdnt =>
                new StudentResult()
                {
                    Surname = stdnt.Surname,
                    Name = stdnt.Name,
                    Patronymic = stdnt.Patronymic,
                    AverageGrade = stdnt.Grades.Average(t => t.Value)
                }).ToList();


            WriteValue(worksheet, 0, 0, "Фамилия");
            WriteValue(worksheet, 0, 1, "Имя");
            WriteValue(worksheet, 0, 2, "Отчество");
            WriteValue(worksheet, 0, 3, "Средний балл");


            for (int i = 0;i < stdntsRslts.Count; i++)
            {
                WriteValue(worksheet, i, 0, stdntsRslts[i].Surname);
                WriteValue(worksheet, i, 1, stdntsRslts[i].Name);
                WriteValue(worksheet, i, 2, stdntsRslts[i].Patronymic);
                WriteValue(worksheet, i, 3, stdntsRslts[i].AverageGrade.ToString("F2"));
            }


            ExcelPackage.SaveAs(new FileInfo(path));
        }

        void WriteValue(ExcelWorksheet excelWorksheet, int row, int column, string value)
            => excelWorksheet.Cells[row + 1, column + 1].Value = value;

    }
}
