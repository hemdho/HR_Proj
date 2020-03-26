using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
    public class AuditLog
    {
        [Key]
        public int Audit_Id { get; set; }
        public int? User_Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string Status { get; set; }
        public int AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
    }
}
