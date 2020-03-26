using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }
        public int? Company_Id { get; set; }
        public int? Site_Id { get; set; }
        public int? JD_Id { get; set; }
        public int? Dept_Id { get; set; }
        public int? Desig_Id { get; set; }
        public int? Zone_Id { get; set; }
        public int? Shift_Id { get; set; }
        [Required,MaxLength(100)]
        public string Emp_Code { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? Reporting_Id { get; set; }
        public Int16? isSponsored { get; set; }
        public Int16? Tupe { get; set; }
        [MaxLength(100)]
        public string NiNo { get; set; }
        [MaxLength(1000)]
        public string NiCategory { get; set; }
        public int? PreviousEmp_Id { get; set; } 
        public Int16? isActive { get; set; }
        public int? AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public IList<Employee_Address> emp_address { get; set; }
        public IList<Employee_Bank> emp_bank { get; set; }
        public Employee_BasicInfo emp_basicinfo { get; set; }
        public IList<Employee_Contact> emp_contact { get; set; }
        public IList<Employee_Contract> emp_contract { get; set; }
        public IList<Employee_Document> emp_document { get; set; }
        public IList<Employee_Emergency> emp_emergency { get; set; }
        public IList<Employee_Probation> emp_probation { get; set; }
        public IList<Employee_Reference> emp_reference { get; set; }
        public IList<Employee_Resignation> emp_resignation { get; set; }
        public IList<Employee_RightToWork> emp_righttowork { get; set; }
        public IList<Employee_Salary> emp_salary { get; set; }
    }
}
