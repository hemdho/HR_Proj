using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_Contract
    {
        [Key]
        public int Emp_Contract_Id { get; set; }
        public int? Contract_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string Emp_Contract_Code { get; set; }
        [MaxLength(500)]
        public string Emp_Contract_Name { get; set; }
        public int? Emp_Contract_HoursDaily { get; set; }
        public int? Emp_Contract_HoursWeekly { get; set; }
        [MaxLength(100)]
        public string Emp_Contract_Days { get; set; }
        [MaxLength(500)]
        public string Emp_Contract_Type { get; set; }
        public DateTime? Emp_Contract_Start { get; set; }
        public DateTime? Emp_Contract_End { get; set; }
        public int? Emp_Doc_Id { get; set; }
        public Int16? isRequired { get; set; }
        [MaxLength(2000)]
        public string Notes { get; set; }
        [MaxLength(20)]
        public string Version_Id { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
