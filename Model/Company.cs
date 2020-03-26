using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
    public class Company
    {
        [Key]
        public int Company_Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Company_Code { get; set; }
        
        [Required, MaxLength(500)]
        public string Company_Name { get; set; }
        
        public int? Company_Parent_Id { get; set; }
        
        [MaxLength(500)]
        public string Registration_No { get; set; }
        
        public DateTime? Registration_Date { get; set; }

        public string Logo { get; set; }

        public string Currency { get; set; }

        [MaxLength(100)]
        public string Language { get; set; }
        
        public Int16 isActive { get; set; }        
       
        public int? AddedBy { get; set; }
        
        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }


        public IEnumerable<Company_Contact> Company_Contact { get; set; }            
    }
}