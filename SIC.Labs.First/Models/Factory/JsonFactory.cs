using SIC.Labs.First.Models.DTO;
using SIC.Labs.First.Services.Interfaces;
using SIC.Labs.First.Services.Writers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.First.Models.Factory
{
    public class JsonFactory : IFactory<IWriter<Student>>
    {
        public IWriter<Student> FactoryMethod()
             => (new WriterJson());
    }
}
