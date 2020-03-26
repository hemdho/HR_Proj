using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        public int? Company_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Dept_Code { get; set; }

        [Required]
        [MaxLength(500)]
        public string Dept_Name { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public Int16 isActive { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        //[ForeignKey("Company_Id")]
        //public virtual Company company { get; set; }

        //public Company company { get; set; }


        public Company company { get; set; }
    }
}
