using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class ContractView : Pagination
    {
        [Key]
        public int Contract_Id { get; set; }
        public int Company_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Contract_Code { get; set; }
        [Required]
        [MaxLength(500)]
        public string Contract_Name { get; set; }

        public int Contract_HoursDaily { get; set; }

        public int Contract_HoursWeekly { get; set; }
        [MaxLength(100)]
        public string Contract_Days { get; set; }
        [MaxLength(500)]
        public string Contract_Type { get; set; }
        [MaxLength(2000)]
        public string Notes { get; set; }
        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
