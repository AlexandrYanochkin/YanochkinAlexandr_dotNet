using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models
{
    public class StockItemViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }

        [Required]
        public int StockId { get; set; }

        public StockViewModel Stock { get; set; }

        public SelectList Stocks { get; set; }

        [Required]
        public int CommodityId { get; set; }

        public CommodityViewModel Commodity { get; set; }

        public SelectList Commodities { get; set; }

    }
}
