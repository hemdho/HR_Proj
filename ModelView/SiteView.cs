using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class SiteView : Pagination
    {
        [Key]
        public int Site_Id { get; set; }

        public int? Company_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Site_Code { get; set; }

        [Required]
        [MaxLength(500)]
        public string Site_Name { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
        [MaxLength(100)]
        public string Address2 { get; set; }
        [MaxLength(100)]
        public string Address3 { get; set; }
        [MaxLength(100)]
        public string Address4 { get; set; }
        [MaxLength(100)]
        public string PostCode { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        public Int16 isActive { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
