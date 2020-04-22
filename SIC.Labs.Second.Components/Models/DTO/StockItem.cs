using System;
using System.Collections.Generic;
using System.Text;

namespace SIC.Labs.Second.Components.Models.DTO
{
    [Serializable]
    public class StockItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int StockId { get; set; }
        public int CommodityId { get; set; }


        public override bool Equals(object obj)
             => (obj is StockItem item &&
                   Count == item.Count &&
                   StockId == item.StockId &&
                   CommodityId == item.CommodityId);

        public override int GetHashCode()
            => HashCode.Combine(Count, StockId, CommodityId);

        public override string ToString()
            => ($"{Count};{StockId};{CommodityId}");
    }
}
