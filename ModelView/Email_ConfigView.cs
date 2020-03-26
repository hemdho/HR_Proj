using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class Email_ConfigView
    {
        [Key]
        public int Email_Config_Id { get; set; }
        public int Company_Id { get; set; }
        public string Email_Host { get; set; }
        public int Email_Port { get; set; }
        public string Email_UserName { get; set; }
        public string Email_Password { get; set; }
        public Int16 EnableSSL { get; set; }
        public Int16 TLSEnable { get; set; }
        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
