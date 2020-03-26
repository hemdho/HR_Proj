using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class ProbationView : Pagination
    {
        [Key]
        public int Prob_Id { get; set; }
        public int Company_Id { get; set; }
        [Required, MaxLength(100)]
        public string Prob_Code { get; set; }
        [Required, MaxLength(500)]
        public string Prob_Name { get; set; }
        [MaxLength(2000)]
        public string Prob_Description { get; set; }
        //[MaxLength(5)]
        public string Prob_DurationUnit { get; set; }

        public int Prob_Duration { get; set; }
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
