using HR.WebApi.DAL;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class EmployeeDetailService
    {
        private readonly ApplicationDbContext adbContext;
        public EmployeeRepository<Employee> employeeRepo;
        public Employee_AddressRepository<Employee_Address> empAddressRepo;
        public Employee_BankRepository<Employee_Bank> empBankRepo;
        public Employee_BasicInfoRepository<Employee_BasicInfo> empBasicInfoRepo;
        public Employee_ContactRepository<Employee_Contact> empContactRepo;
        public Employee_ContractRepository<Employee_Contract> empContractRepo;
        public Employee_DocumentRepository<Employee_Document> empDocumentRepo;
        public Employee_EmergencyRepository<Employee_Emergency> empEmergencyRepo;
        public Employee_ProbationRepository<Employee_Probation> empProbationRepo;
        public Employee_ReferenceRepository<Employee_Reference> empReferenceRepo;
        public Employee_ResignationRepository<Employee_Resignation> empResignationRepo;
        public Employee_RightToWorkRepository<Employee_RightToWork> empRightToWorkRepo;
        public Employee_SalaryRepository<Employee_Salary> empSalaryRepo;
        public EmployeeDetailService(EmployeeRepository<Employee> empRepository, Employee_AddressRepository<Employee_Address> emp_AddressRepository,
            Employee_BankRepository<Employee_Bank> emp_BankRepository, Employee_BasicInfoRepository<Employee_BasicInfo> emp_BasicinfoRepository,
            Employee_ContactRepository<Employee_Contact> emp_ContactRepository, Employee_ContractRepository<Employee_Contract> emp_ContractRepository,
            Employee_DocumentRepository<Employee_Document> emp_DocumentRepository, Employee_EmergencyRepository<Employee_Emergency> emp_EmergencyRepository,
            Employee_ProbationRepository<Employee_Probation> emp_ProbationRepository, Employee_ReferenceRepository<Employee_Reference> emp_ReferenceRepository,
            Employee_ResignationRepository<Employee_Resignation> emp_ResignationRepository, Employee_RightToWorkRepository<Employee_RightToWork> emp_RighttoworkRepository,
            Employee_SalaryRepository<Employee_Salary> emp_SalaryRepository)
        {
            adbContext = Startup.applicationDbContext;
            employeeRepo = empRepository;
            empAddressRepo = emp_AddressRepository;
            empBankRepo = emp_BankRepository;
            empBasicInfoRepo = emp_BasicinfoRepository;
            empContactRepo = emp_ContactRepository;
            empContractRepo = emp_ContractRepository;
            empDocumentRepo = emp_DocumentRepository;
            empEmergencyRepo = emp_EmergencyRepository;
            empProbationRepo = emp_ProbationRepository;
            empReferenceRepo = emp_ReferenceRepository;
            empResignationRepo = emp_ResignationRepository;
            empRightToWorkRepo = emp_RighttoworkRepository;
            empSalaryRepo = emp_SalaryRepository;
        }
        public async Task<IEnumerable<Employee>> GetAll(int RecordLimit)
        {
            var vlist = await employeeRepo.GetAll(RecordLimit);
            await empAddressRepo.GetAll(RecordLimit);
            await empBankRepo.GetAll(RecordLimit);
            await empBasicInfoRepo.GetAll(RecordLimit);
            await empContactRepo.GetAll(RecordLimit);
            await empContractRepo.GetAll(RecordLimit);
            await empDocumentRepo.GetAll(RecordLimit);
            await empEmergencyRepo.GetAll(RecordLimit);
            await empProbationRepo.GetAll(RecordLimit);
            await empReferenceRepo.GetAll(RecordLimit);
            await empResignationRepo.GetAll(RecordLimit);
            await empRightToWorkRepo.GetAll(RecordLimit);
            await empSalaryRepo.GetAll(RecordLimit);
            return vlist;
        }
        public async Task<IEnumerable<Employee>> GetEmployee(int id)
        {
            try
            {
                var vlist = await employeeRepo.Get(id);
                await empAddressRepo.GetByEmp_Id(id);
                await empBankRepo.GetByEmp_Id(id);
                await empBasicInfoRepo.GetByEmp_Id(id);
                await empContactRepo.GetByEmp_Id(id);
                await empContractRepo.GetByEmp_Id(id);
                await empDocumentRepo.GetByEmp_Id(id);
                await empEmergencyRepo.GetByEmp_Id(id);
                await empProbationRepo.GetByEmp_Id(id);
                await empReferenceRepo.GetByEmp_Id(id);
                await empResignationRepo.GetByEmp_Id(id);
                await empRightToWorkRepo.GetByEmp_Id(id);
                await empSalaryRepo.GetByEmp_Id(id);
                return vlist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task Insert(Employee entity)
        {
            adbContext.BeginTransaction();
            try
            {
                _ = employeeRepo.Insert(entity);
                await empAddressRepo.Insert_Multiple(entity.emp_address);
                await empBankRepo.Insert_Multiple(entity.emp_bank);
                await empBasicInfoRepo.Insert(entity.emp_basicinfo);
                await empContactRepo.Insert_Multiple(entity.emp_contact);
                await empContractRepo.Insert_Multiple(entity.emp_contract);
                await empDocumentRepo.Insert_Multiple(entity.emp_document);
                await empEmergencyRepo.Insert_Multiple(entity.emp_emergency);
                await empProbationRepo.Insert_Multiple(entity.emp_probation);
                await empReferenceRepo.Insert_Multiple(entity.emp_reference);
                await empResignationRepo.Insert_Multiple(entity.emp_resignation);
                await empRightToWorkRepo.Insert_Multiple(entity.emp_righttowork);
                await empSalaryRepo.Insert_Multiple(entity.emp_salary);
                adbContext.CommitTransaction();
            }

            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }
        public async Task Update(Employee entity)
        {
            adbContext.BeginTransaction();
            try
            {
                await employeeRepo.Update(entity);
                await empAddressRepo.Update_Addresses(entity.emp_address);
                await empBankRepo.Update_BankDetails(entity.emp_bank);
                await empBasicInfoRepo.Update(entity.emp_basicinfo);
                await empContactRepo.Update_Contact(entity.emp_contact);
                await empContractRepo.Update_Contract(entity.emp_contract);
                await empDocumentRepo.Update_Document(entity.emp_document);
                await empEmergencyRepo.Update_EmergencyDetails(entity.emp_emergency);
                await empProbationRepo.Update_Probation(entity.emp_probation);
                await empReferenceRepo.Update_Reference(entity.emp_reference);
                await empResignationRepo.Update_Resignation(entity.emp_resignation);
                await empRightToWorkRepo.Update_RightToWork(entity.emp_righttowork);
                await empSalaryRepo.Update_Salary(entity.emp_salary);
                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            adbContext.BeginTransaction();
            try
            {                
                await empAddressRepo.DeleteByEmp_Id(id);
                await empBankRepo.DeleteByEmp_Id(id);
                await empBasicInfoRepo.DeleteByEmp_Id(id);
                await empContactRepo.DeleteByEmp_Id(id);
                await empContractRepo.DeleteByEmp_Id(id);
                await empDocumentRepo.DeleteByEmp_Id(id);
                await empEmergencyRepo.DeleteByEmp_Id(id);
                await empProbationRepo.DeleteByEmp_Id(id);
                await empReferenceRepo.DeleteByEmp_Id(id);
                await empResignationRepo.DeleteByEmp_Id(id);
                await empRightToWorkRepo.DeleteByEmp_Id(id);
                await empSalaryRepo.DeleteByEmp_Id(id);
                await employeeRepo.Delete(id);
                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }
        public async Task ToogleStatus(int id, short isActive)
        {
            adbContext.BeginTransaction();
            try
            {
                await employeeRepo.ToogleStatus(id, isActive);
                await empAddressRepo.ToogleStatusByEmp_Id(id, isActive);
                await empBankRepo.ToogleStatusByEmp_Id(id, isActive);
                await empBasicInfoRepo.ToogleStatusByEmp_Id(id, isActive);
                await empContactRepo.ToogleStatusByEmp_Id(id, isActive);
                await empContractRepo.ToogleStatusByEmp_Id(id, isActive);
                await empDocumentRepo.ToogleStatusByEmp_Id(id, isActive);
                await empEmergencyRepo.ToogleStatusByEmp_Id(id, isActive);
                await empProbationRepo.ToogleStatusByEmp_Id(id, isActive);
                await empReferenceRepo.ToogleStatusByEmp_Id(id, isActive);
                await empResignationRepo.ToogleStatusByEmp_Id(id, isActive);
                await empRightToWorkRepo.ToogleStatusByEmp_Id(id, isActive);
                await empSalaryRepo.ToogleStatusByEmp_Id(id, isActive);
                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

    }
}
