using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength(500)]
        public string Category { get; set; }
        [MaxLength(2000)]
        public string Notes { get; set; }

        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
