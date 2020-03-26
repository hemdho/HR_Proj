using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class ShiftView : Pagination
    {
        [Key]
        public int Shift_Id { get; set; }
        public int Company_Id { get; set; }
        [Required, MaxLength(100)]
        public string Shift_Code { get; set; }
        [Required, MaxLength(500)]
        public string Shift_Name { get; set; }
        [MaxLength(5)]
        public string Shift_Start { get; set; }
        [MaxLength(5)]
        public string Shift_End { get; set; }
        public Int16 NightShift { get; set; }
        public Int16 Shift_Variable { get; set; }
        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
