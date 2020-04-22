using Newtonsoft.Json;
using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIC.Labs.Second.Components.Services.Writers.JsonWriters
{
    public class JsonSerializer<T> : IWriter<T>
    {
        public void Write(string path, IEnumerable<T> collection)
        {
            path.ValidateJsonPath();

            using(StreamWriter streamWriter = new StreamWriter(path))
            {
                JsonSerializer jsonSerializer = new JsonSerializer() { Formatting = Formatting.Indented };

                jsonSerializer.Serialize(streamWriter, collection);
            }
                     
        }
    }
}
