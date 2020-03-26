using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_BasicInfo
    {
        [Key]
        public int BasicInfo_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string MiddleName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        //public string Photo { get; set; }
        public DateTime? DOB { get; set; }
        [MaxLength(100)]
        public string Gender { get; set; }
        [MaxLength(100)]
        public string BloodGroup { get; set; }
        [MaxLength(100)]
        public string Nationality { get; set; }
        [MaxLength(100)]
        public string Ethnicity_Code { get; set; }
        [MaxLength(20)]
        public string Version_Id { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
