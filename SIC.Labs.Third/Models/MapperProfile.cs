using AutoMapper;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Third.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Commodity, CommodityViewModel>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerViewModel>().ReverseMap();

            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

            CreateMap<Stock, StockViewModel>().ReverseMap();

            CreateMap<StockItem, StockItemViewModel>().ReverseMap();

            CreateMap<Order, OrderViewModel>().ReverseMap();
        }

    }
}
