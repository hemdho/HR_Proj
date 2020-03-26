using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
    public class Designation
    {
        [Key]
        public int Desig_Id { get; set; }        
        public int Company_Id { get; set; }        
        public int Dept_Id { get; set; }        
        public int Zone_Id { get; set; }
        [Required,MaxLength(100)]
        public string Desig_Code { get; set; }
        [Required,MaxLength(500)]
        public string Desig_Name { get; set; }
        [MaxLength(100)]
        public string Report_To { get; set; }
        [MaxLength(100)]
        public string AdditionalReport_To { get; set; }
        public int? Level { get; set; }
        
        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Company company { get; set; }
        
        public Department department { get; set; }
        
        public Zone zone { get; set; }

        //[ForeignKey("Company_Id")]
        //public virtual Company Companies { get; set; }
    }
}