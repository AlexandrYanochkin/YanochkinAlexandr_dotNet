using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public OrderStatus Status { get; set; }
        public int Count { get; set; }
        public decimal FullPrice { get; set; }
        public int EmployeeId { get; set; }
        public int StockItemId { get; set; }



        public override bool Equals(object obj)
            => (obj is Order order &&
                   CreationDate == order.CreationDate &&
                   ModificationDate == order.ModificationDate &&
                   Status == order.Status &&
                   Count == order.Count &&
                   FullPrice == order.FullPrice &&
                   EmployeeId == order.EmployeeId &&
                   StockItemId == order.StockItemId);

        public override int GetHashCode()
            => HashCode.Combine(CreationDate, ModificationDate, Status, Count, FullPrice, EmployeeId, StockItemId);

        public override string ToString()
            => ($"{CreationDate.ToString("dd.MM.yyyy")};{ModificationDate.ToString("dd.MM.yyyy")};{Count};{FullPrice};{Status};{EmployeeId};{StockItemId}");
    }
}
