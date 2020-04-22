using SIC.Labs.Second.Components.Services.Interfaces;
using SIC.Labs.Second.Components.Services.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SIC.Labs.Second.Components.Services.Readers
{
    public class XmlDeserializer<T> : IReader<T>
    {
        public IEnumerable<T> Read(string path)
        {
            path.ValidateXmlPath();
            path.CheckFileExistance();

            IEnumerable<T> collection = null;

            using (StreamReader streamReader = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

                collection = (xmlSerializer.Deserialize(streamReader) as IEnumerable<T>);
            }

            return collection;
        }
    }
}
