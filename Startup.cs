using HR.WebApi.ActionFilters;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using HR.WebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Xml;

namespace HR.WebApi
{
    public class Startup
    {
        public static ApplicationDbContext applicationDbContext;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            applicationDbContext = new ApplicationDbContext();
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>();
            //services.AddDbContext<ApplicationDbContext>(new LifetimeContainer(), ServiceLifetime.Transient());

            #region All Repository List

            services.AddTransient(typeof(ICommonQuery<SiteView>), typeof(SiteRepository<SiteView>));
            services.AddTransient(typeof(ICommonQuery<ZoneView>), typeof(ZoneRepository<ZoneView>));
            services.AddTransient(typeof(ICommonQuery<DepartmentView>), typeof(DepartmentRepository<DepartmentView>));
            services.AddTransient(typeof(ICommonQuery<DesignationView>), typeof(DesignationRepository<DesignationView>));
            services.AddTransient(typeof(ICommonQuery<Job_DiscriptionView>), typeof(Job_DescriptionRepository<Job_DiscriptionView>));
            services.AddTransient(typeof(ICommonQuery<ProbationView>), typeof(ProbationRepository<ProbationView>));
            services.AddTransient(typeof(ICommonQuery<Module_PermissionView>), typeof(Module_PermissionRepository<Module_PermissionView>));
            services.AddTransient(typeof(ICommonQuery<ShiftView>), typeof(ShiftRepository<ShiftView>));
            services.AddTransient(typeof(ICommonQuery<Email_ConfigView>), typeof(EmailConfigRepository<Email_ConfigView>));

            services.AddTransient(typeof(ICommonRepository<DepartmentView>), typeof(DepartmentRepository<DepartmentView>));
            services.AddTransient(typeof(ICommonRepository<ShiftView>), typeof(ShiftRepository<ShiftView>));
            services.AddTransient(typeof(ICommonRepository<SiteView>), typeof(SiteRepository<SiteView>));
            services.AddTransient(typeof(ICommonRepository<ZoneView>), typeof(ZoneRepository<ZoneView>));
            services.AddTransient(typeof(ICommonRepository<ContractView>), typeof(ContractRepository<ContractView>));
            services.AddTransient(typeof(ICommonRepository<DesignationView>), typeof(DesignationRepository<DesignationView>));
            services.AddTransient(typeof(ICommonRepository<ProbationView>), typeof(ProbationRepository<ProbationView>));
            services.AddTransient(typeof(ICommonRepository<Ethinicity>), typeof(EthinicityRepository<Ethinicity>));
            services.AddTransient(typeof(ICommonRepository<Marital_status>), typeof(Marital_StatusRepository<Marital_status>));
            services.AddTransient(typeof(ICommonRepository<Job_DiscriptionView>), typeof(Job_DescriptionRepository<Job_DiscriptionView>));
            services.AddTransient(typeof(ICommonRepository<Email_ConfigView>), typeof(EmailConfigRepository<Email_ConfigView>));
            services.AddTransient(typeof(ICommonRepository<Document>), typeof(DocumentRepository<Document>));
            services.AddTransient(typeof(ICommonRepository<Module_PermissionView>), typeof(Module_PermissionRepository<Module_PermissionView>));

            services.AddTransient(typeof(IDocuments), typeof(Common.Documents));
            services.AddTransient(typeof(ICommonRepository<Company>), typeof(CompanyRepository<Company>));
            services.AddTransient(typeof(ICommonRepository<Company_Contact>), typeof(Company_ContactRepository<Company_Contact>));
            services.AddTransient(typeof(Company_ContactRepository<Company_Contact>));

            services.AddTransient(typeof(IUserService<User>), typeof(UserService<User>));
            services.AddTransient(typeof(Common.TokenService));
            services.AddTransient(typeof(User_PasswordRepository));
            services.AddTransient(typeof(IUser_PasswordReset), typeof(User_PasswordResetRepository));
            services.AddTransient(typeof(IUserService<User>), typeof(UserService<User>));

            #region Employee Repository List

            services.AddTransient(typeof(EmployeeDetailService));
            services.AddTransient(typeof(EmployeeRepository<Employee>));
            services.AddTransient(typeof(Employee_AddressRepository<Employee_Address>));
            services.AddTransient(typeof(Employee_BankRepository<Employee_Bank>));
            services.AddTransient(typeof(Employee_BasicInfoRepository<Employee_BasicInfo>));
            services.AddTransient(typeof(Employee_ContactRepository<Employee_Contact>));
            services.AddTransient(typeof(Employee_ContractRepository<Employee_Contract>));
            services.AddTransient(typeof(Employee_DocumentRepository<Employee_Document>));
            services.AddTransient(typeof(Employee_EmergencyRepository<Employee_Emergency>));
            services.AddTransient(typeof(Employee_ProbationRepository<Employee_Probation>));
            services.AddTransient(typeof(Employee_ReferenceRepository<Employee_Reference>));
            services.AddTransient(typeof(Employee_ResignationRepository<Employee_Resignation>));
            services.AddTransient(typeof(Employee_RightToWorkRepository<Employee_RightToWork>));
            services.AddTransient(typeof(Employee_SalaryRepository<Employee_Salary>));

            services.AddTransient(typeof(ICommonRepository<Employee>),typeof(EmployeeRepository<Employee>));
            services.AddTransient(typeof(ICommonRepository<Employee_Address>),typeof(Employee_AddressRepository<Employee_Address>));
            services.AddTransient(typeof(ICommonRepository<Employee_Bank>),typeof(Employee_BankRepository<Employee_Bank>));
            services.AddTransient(typeof(ICommonRepository<Employee_BasicInfo>),typeof(Employee_BasicInfoRepository<Employee_BasicInfo>));
            services.AddTransient(typeof(ICommonRepository<Employee_Contact>),typeof(Employee_ContactRepository<Employee_Contact>));
            services.AddTransient(typeof(ICommonRepository<Employee_Contract>),typeof(Employee_ContractRepository<Employee_Contract>));
            services.AddTransient(typeof(ICommonRepository<Employee_Document>),typeof(Employee_DocumentRepository<Employee_Document>));
            services.AddTransient(typeof(ICommonRepository<Employee_Emergency>),typeof(Employee_EmergencyRepository<Employee_Emergency>));
            services.AddTransient(typeof(ICommonRepository<Employee_Probation>),typeof(Employee_ProbationRepository<Employee_Probation>));
            services.AddTransient(typeof(ICommonRepository<Employee_Reference>),typeof(Employee_ReferenceRepository<Employee_Reference>));
            services.AddTransient(typeof(ICommonRepository<Employee_Resignation>),typeof(Employee_ResignationRepository<Employee_Resignation>));
            services.AddTransient(typeof(ICommonRepository<Employee_RightToWork>),typeof(Employee_RightToWorkRepository<Employee_RightToWork>));
            services.AddTransient(typeof(ICommonRepository<Employee_Salary>), typeof(Employee_SalaryRepository<Employee_Salary>));

            #endregion Employee Repository List

            services.AddTransient(typeof(ICommonRepository<User_Role>), typeof(User_RoleRepository<User_Role>));
            services.AddTransient(typeof(ICommonRepository<Roles>), typeof(RolesRepository<Roles>));

            services.AddTransient(typeof(ICommonRepository<Model.Module>), typeof(ModuleRepository<Model.Module>));
            #endregion All Repository List

            #region Jwt Auth
            //Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //JWT Auth
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            IConfigurationRoot objConfig = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = objConfig.GetValue<string>("JwtAuth:IssuedBy"),

                    ValidateAudience = true,
                    ValidAudience = objConfig.GetValue<string>("JwtAuth:Audience"),

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(objConfig.GetValue<string>("JwtAuth:Secret"))),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Create token from HR.CommonUtility class
            //Implement below config from json to the CommonUtility class & Startup.cs here
            //JwtAuth:IssuedBy, JwtAuth:Secret, JwtAuth:Secret, JwtAuth:Audience, JwtAuth:ExpiryTime

            #endregion Jwt Auth

            #region Logger
            services.AddScoped<Log>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Log));
            });
            #endregion Logger

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://example.com", "http://www.contoso.com"); //Pending = site access,issuedby,authorised domain include in this
                    builder.WithHeaders("USER_ID", "LOGIN_ID", "COMPANY_ID", "TOKEN_NO");
                    builder.WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logger
            XmlDocument xdDoc = new XmlDocument();
            xdDoc.Load(File.OpenRead("log4net.config"));
            loggerFactory.AddLog4Net(Convert.ToString(xdDoc["log4net"]), true);
            #endregion Logger

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
