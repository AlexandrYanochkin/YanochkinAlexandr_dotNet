using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIC.Labs.Third.Models.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [Range(18, 75)]
        public int Age { get; set; }
        
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
