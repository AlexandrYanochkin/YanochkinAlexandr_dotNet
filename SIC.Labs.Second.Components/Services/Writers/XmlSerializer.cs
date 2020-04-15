using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SIC.Labs.Second.Components.Services.Writers.XmlWriters
{
    public class XmlSerializer<T> : IWriter<T>
    {
        public void Write(string path, IEnumerable<T> collection)
        {
            path.ValidateXmlPath();

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

                xmlSerializer.Serialize(streamWriter, collection.ToList());
            }
        }
    }
}
