using System;
using CommandLine;

namespace SIC.Labs.First.Models.DTO
{
    public class Options
    {
        [Option('i', "input", Required = true)]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true)]
        public string OutputFile { get; set; }

        [Option('f', "format", Required = true)]
        public string Format { get; set; }

    }
}
