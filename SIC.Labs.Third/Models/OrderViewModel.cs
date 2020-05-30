using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models
{
    public class OrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ModificationDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public SelectList Statuses { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal FullPrice { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public EmployeeViewModel Employee { get; set; }

        public SelectList Employees { get; set; }

        [Required]
        public int StockItemId { get; set; }

        public StockItemViewModel StockItem { get; set; }

        public SelectList StockItems { get; set; }
    }
}
