using HR.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace HR.WebApi.DAL
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot objConfig = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();

                string strDbType = objConfig.GetValue<string>("DataBase:ConnectionType");

                MySqlConnection objConn = new MySqlConnection(optionsBuilder);
                objConn.ConnectionString = objConfig.GetConnectionString(strDbType);
                optionsBuilder = objConn.GetConn();
            }
        }

        public void BeginTransaction()
        {
            base.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            base.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            base.Database.RollbackTransaction();
        }

        public DbSet<Company> company { get; set; }
        public DbSet<Company_Contact> company_contact { get; set; }
        public DbSet<Contract> contract { get; set; }
        public DbSet<Email_Config> email_config { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Designation> designation { get; set; }
        public DbSet<Ethinicity> ethinicity { get; set; }
        public DbSet<Marital_status> marital_status { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Employee_Address> employee_address { get; set; }
        public DbSet<Employee_Bank> employee_bank { get; set; }
        public DbSet<Employee_BasicInfo> employee_basicinfo { get; set; }
        public DbSet<Employee_Contact> employee_contact { get; set; }
        public DbSet<Employee_Contract> employee_contract { get; set; }
        public DbSet<Employee_Document> employee_document { get; set; }
        public DbSet<Employee_Emergency> employee_emergency { get; set; }
        public DbSet<Employee_Reference> employee_reference { get; set; }
        public DbSet<Employee_RightToWork> employee_righttowork { get; set; }
        public DbSet<Employee_Probation> employee_probation { get; set; }
        public DbSet<Employee_Resignation> employee_resignation { get; set; }
        public DbSet<Employee_Salary> employee_salary { get; set; }
        public DbSet<Probation> probation { get; set; }
        public DbSet<Shift> shift { get; set; }
        public DbSet<Site> site { get; set; }
        public DbSet<Zone> zone { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<User_Password> user_password { get; set; }
        public DbSet<User_PasswordReset> user_passwordreset { get; set; }
        public DbSet<Job_Description>  job_description { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Role_Permission> role_permission { get; set; }
        public DbSet<Role_Menu_Link_Old> role_menu_link_old { get; set; }
        public DbSet<User_Role> user_role { get; set; }
        public DbSet<Document> document { get; set; }
        public DbSet<User_Token> user_token { get; set; }
        public DbSet<UserLog> userlog { get; set; }
        public DbSet<AuditLog> auditlog { get; set; }
        public DbSet<Module> module { get; set; }
        public DbSet<Module_Permission> module_permission { get; set; }

    }
}
