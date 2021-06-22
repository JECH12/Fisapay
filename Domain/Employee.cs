using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Domain
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public bool Covid_Vaccine { get; set; }
    }
}
