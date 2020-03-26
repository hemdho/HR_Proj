using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_Contact
    {
        [Key]
        public int Emp_Contact_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string Contact_Type { get; set; }
        [MaxLength(100)]
        public string Contact_Value { get; set; }
        public Int16? isDefault { get; set; }
        [MaxLength(20)]
        public string Version_Id { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
