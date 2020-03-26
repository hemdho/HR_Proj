using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_Bank
    {
        [Key]
        public int Emp_Bank_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string Bank_Code { get; set; }
        [MaxLength(500)]
        public string Bank_Name { get; set; }
        public int? Account_Title { get; set; }
        public int? Account_No { get; set; }
        [MaxLength(100)]
        public string Account_Holder { get; set; }
        [MaxLength(500)]
        public string Account_Type { get; set; }
        [MaxLength(500)]
        public string Account_Code { get; set; }
        public Int16? isPayed { get; set; }
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
