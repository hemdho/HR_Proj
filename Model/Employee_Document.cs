using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_Document
    {
        [Key]
        public int Emp_Doc_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        public int? Doc_Id { get; set; }
        public int? Parent_Emp_Doc_Id { get; set; }        
        [MaxLength(500)]
        public string Emp_Doc_Name { get; set; }
        [MaxLength(2000)]
        public string Doc_Path { get; set; }
        [MaxLength(2000)]
        public string Notes { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
