using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.ModelView
{
    public class Job_DiscriptionView : Pagination
    {
        [Key]
        public int JD_Id { get; set; }
        public int Company_Id { get; set; }
        public int Dept_Id { get; set; }
        public int Desig_Id { get; set; }
        [Required, MaxLength(100)]
        public string JD_Code { get; set; }
        [Required, MaxLength(500)]
        public string JD_Name { get; set; }
        [MaxLength(2000)]
        public string JD_Description { get; set; }
        [MaxLength(2000)]
        public string Notes { get; set; }

        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
        public string Dept_Name { get; set; }
        public string Desig_Name { get; set; }

    }
}
