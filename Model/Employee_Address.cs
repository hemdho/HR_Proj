using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Employee_Address
    {
        [Key]
        public int Emp_Address_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        [MaxLength(100)]
        public string Address_Type { get; set; }
        [MaxLength(200)]
        public string Address1 { get; set; }
        [MaxLength(200)]
        public string Address2 { get; set; }
        [MaxLength(200)]
        public string Address3 { get; set; }
        [MaxLength(200)]
        public string Address4 { get; set; }
        [MaxLength(200)]
        public string PostCode { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(200)]
        public string State { get; set; }
        [MaxLength(200)]
        public string Country { get; set; }
        [MaxLength(20)]
        public string LandlineNo { get; set; }
        public Int16? isDefault { get; set; }
        public int? Emp_Doc_Id { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
