using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIC.Labs.First.Services.Validators
{
    public static class PathValidator
    {
        public static void ValidateJsonPath(this string path)
            => ValidatePath(path, ".json");

        public static void ValidateExcelPath(this string path)
            => ValidatePath(path, ".xlsx");

        public static void ValidateCsvPath(this string path)
            => ValidatePath(path, ".csv");

        public static void CheckCsvFileExist(this string path)
            => CheckExistance(path, ".csv");

        static void ValidatePath(string path, string format)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path can't be null or empty");

            if (!path.EndsWith(format))
                throw new ArgumentException($"File should be in {format} format");
        }

        static void CheckExistance(string path,string format)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path);
        }


    }
}
