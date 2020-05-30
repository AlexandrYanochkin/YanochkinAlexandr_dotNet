using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models
{
    public class CommodityViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int ManufacturerId { get; set; }


        public ManufacturerViewModel Manufacturer{ get; set; }

        public SelectList Manufacturers { get; set; }
    }
}
