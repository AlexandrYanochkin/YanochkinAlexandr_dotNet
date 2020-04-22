using Newtonsoft.Json;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Readers
{
    public class JsonDeserializer<T> : IReader<T>
    {
        public IEnumerable<T> Read(string path)
        {
            path.ValidateJsonPath();
            path.CheckFileExistance();

            IEnumerable<T> collection = null;

            using (StreamReader streamReader = new StreamReader(path))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();

                collection = (jsonSerializer.Deserialize(streamReader, typeof(IEnumerable<T>)) as IEnumerable<T>);
            }

            return collection;
        }
    }
}
