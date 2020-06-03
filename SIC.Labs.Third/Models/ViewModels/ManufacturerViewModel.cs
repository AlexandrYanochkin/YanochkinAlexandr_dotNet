using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models.ViewModels
{
    public class ManufacturerViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Address { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

    }
}
