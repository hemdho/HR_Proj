using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.ModelView
{
    public class DepartmentView: Pagination
    {
        [Key]
        public int? Dept_Id { get; set; }
        public int? Company_Id { get; set; }

        [Required]
        public string Dept_Code { get; set; }

        [Required]
        public string Dept_Name { get; set; }
        public string Notes { get; set; }
        public short isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
