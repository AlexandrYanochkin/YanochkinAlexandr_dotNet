using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.DAL.MSSQLRepository;
using SIC.Labs.Second.Components.Services.Interfaces;

namespace SIC.Labs.Second.Components.Models.Factory
{
    public class MSSQLFactory : IFactory<DAO>
    {
        private readonly string connectionString;
        
        public MSSQLFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DAO FactoryMethod()
            => new DAO(
                new StockRepository(connectionString),
                new StockItemRepository(connectionString),
                new OrderRepository(connectionString),
                new EmployeeRepository(connectionString),
                new CommodityRepository(connectionString),
                new ManufacturerRepository(connectionString));
    }
}
