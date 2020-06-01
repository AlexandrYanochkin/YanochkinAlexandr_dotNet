using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Services.Interfaces;

namespace SIC.Labs.Second.Components.DAL
{
    public class DAO
    {      
        public IRepository<Stock> Stocks { get; set; }
        public IRepository<StockItem> StockItems { get; set; }
        public IRepository<Order> Orders { get; set; }
        public IRepository<Employee> Employees { get; set; }
        public IRepository<Commodity> Commodities { get; set; }
        public IRepository<Manufacturer> Manufacturers { get; set; }
         

        public DAO(IRepository<Stock> stocks,
            IRepository<StockItem> stockItems,
            IRepository<Order> orders,
            IRepository<Employee> employees,
            IRepository<Commodity> commodities,
            IRepository<Manufacturer> manufacturers)
        {
            Stocks = stocks;
            StockItems = stockItems;
            Orders = orders;
            Employees = employees;
            Commodities = commodities;
            Manufacturers = manufacturers;
        }

    }
}
