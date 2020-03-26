using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_Emergency
    {
        [Key]
        public int Emp_Emergency_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string ContactName { get; set; }
        [MaxLength(100)]
        public string ContactNo { get; set; }
        [MaxLength(500)]
        public string Relationship { get; set; }
        public Int16? isDefault { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
