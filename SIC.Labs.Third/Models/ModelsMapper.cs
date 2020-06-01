using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models
{
    public static class ModelsMapper
    {
        public static TModel Map<T, TModel>(this T item)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<T, TModel>()));

            return mapper.Map<T, TModel>(item);
        }

        public static TModel Map<T, TModel>(this T item, MapperConfiguration config)
        {
            var mapper = new Mapper(config);

            return mapper.Map<T, TModel>(item);
        }

        public static IEnumerable<TModel> MapCollection<T, TModel>(this IEnumerable<T> item)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<T, TModel>()));

            return mapper.Map<List<TModel>>(item);
        }

        public static IEnumerable<TModel> MapCollection<T, TModel>(this IEnumerable<T> item, MapperConfiguration config)
        {
            var mapper = new Mapper(config);

            return mapper.Map<List<TModel>>(item);
        }

    }
}
