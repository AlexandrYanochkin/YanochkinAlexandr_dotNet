using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Validators
{
    public static class PathValidators
    {
        public static void ValidateJsonPath(this string path)
            => ValidatePath(path, ".json");

        public static void ValidateBinaryPath(this string path)
            => ValidatePath(path, ".dat");

        public static void ValidateXmlPath(this string path)
            => ValidatePath(path, ".xml");

        private static void ValidatePath(string path,string format)
        {
            if (string.IsNullOrEmpty(path) || !path.EndsWith(format))
                throw new ArgumentException("Incorrect path!!!");

            if(!path.EndsWith(format))
                throw new ArgumentException($"File should be in {format} format!!!");
        }

        public static void CheckFileExistance(this string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Incorrect argument!!!");

            if (!File.Exists(path))
                throw new FileNotFoundException("File not found!!!");
        }

    }
}
