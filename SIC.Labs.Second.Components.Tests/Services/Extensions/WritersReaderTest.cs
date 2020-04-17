using SIC.Labs.Second.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SIC.Labs.Second.Components.Tests.ReadersWritersTests
{
    public static class WritersReadersTestExtensions
    {
        public static bool WriteValuesAndCheckExistance<T>(this IWriter<T> writer, string path,  IEnumerable<T> collection)
        {
            writer.Write(path, collection);

            return File.Exists(path);
        }

        public static bool ReadValuesAndCompareWithCollection<T>(this IReader<T> reader, string path, IEnumerable<T> collectionForComp)
        {
            var collection = reader.Read(path);

            return (collection.Count() == collectionForComp.Count() && 
                collection.Zip(collectionForComp, (fVal, sVal) => new { fVal, sVal }).All(t => t.fVal.Equals(t.sVal)));
        }

    }
}
