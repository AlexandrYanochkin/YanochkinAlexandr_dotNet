using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models.ViewModels
{
    public class StockViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Address { get; set; }
        [Required]
        [Range(0.0, 1.0)]
        public double Allowance { get; set; }
    }
}
