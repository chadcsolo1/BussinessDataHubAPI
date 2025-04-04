using API_GraphQL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_GraphQL.Data
{
    public class Mutation
    {
        public async Task<Employee> CreateEmployee([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context, string nationalIdNumber, string loginID,
            short organizationLevel, string jobTitle, DateOnly birthDate, string maritalStatus, string gender, DateOnly hireDate, 
            int salariedFlag, short vacationHours, short sickLeaveHours, int currentFlag)
        {
            //int businessEntityId, 
            //SalesPerson newSalesPerson = new SalesPerson
            //{
            //    //BusinessEntityId = businessEntityId,
            //    TerritoryId = territoryId,
            //    SalesQuota = salesQuota,
            //    Bonus = bonus,
            //    CommissionPct = commissionPct,
            //    SalesYtd = salesYTD,
            //    SalesLastYear = salesLastYear,
            //    Rowguid = new Guid(),
            //    ModifiedDate = DateTime.UtcNow
            //};
            var businessEntityIdList = await _context.Employees.ToListAsync();
            var bussinessEntityId = businessEntityIdList.OrderByDescending(x => x.BusinessEntityId).FirstOrDefault();

            Employee newEmployee = new Employee
            {
                BusinessEntityId = bussinessEntityId.BusinessEntityId + 1,
                NationalIdnumber = nationalIdNumber,
                LoginId = loginID,
                OrganizationLevel = organizationLevel,
                JobTitle = jobTitle,
                BirthDate = birthDate,
                MaritalStatus = maritalStatus,
                Gender = gender,
                HireDate = hireDate,
                SalariedFlag = salariedFlag == 1,
                VacationHours = vacationHours,
                SickLeaveHours = sickLeaveHours,
                CurrentFlag = currentFlag == 1,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow

            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee;
        }

        //public async Task<SalesPerson> CreateSalesPerson([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context,
        //        [FromBody] SalesPerson salesPerson)
        //{
        //    SalesPerson newSalesPerson = new SalesPerson
        //    {
        //        TerritoryId = salesPerson.TerritoryId,
        //        SalesQuota = salesPerson.SalesQuota,
        //        Bonus = salesPerson.Bonus,
        //        CommissionPct = salesPerson.CommissionPct,
        //        SalesYtd = salesPerson.SalesYtd,
        //        SalesLastYear = salesPerson.SalesLastYear,
        //        Rowguid = salesPerson.Rowguid,
        //        ModifiedDate = DateTime.UtcNow
        //    };

        //    _context.SalesPeople.Add(newSalesPerson);
        //    await _context.SaveChangesAsync();
        //    return newSalesPerson;
        //}

        public async Task<SalesPerson> UpdateSalesPerson([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context,
            decimal bonus, int businessEntityId)
        {
            var salesPerson = await _context.SalesPeople.Where(x => x.BusinessEntityId == businessEntityId).FirstOrDefaultAsync();

            if (salesPerson != null)
            {
                salesPerson.Bonus = bonus;
            }

            await _context.SaveChangesAsync();

            return salesPerson;
        }

        public async Task<DeleteEntityResponse> DeleteSalesPerson([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context, int businessEntityId)
        {
            DeleteEntityResponse deleteEntityResponse = new();
            var salesPerson = await _context.SalesPeople.FirstOrDefaultAsync(x => x.BusinessEntityId == businessEntityId);

            if (salesPerson == null)
            {
                deleteEntityResponse.Status = false;
                deleteEntityResponse.Message = "The targeted entity was not deleted.";
                return deleteEntityResponse;
            }

            _context.SalesPeople.Remove(salesPerson);
            await _context.SaveChangesAsync();

            deleteEntityResponse.Status = true;
            deleteEntityResponse.Message = $"The intended entity {businessEntityId} with a type of was deleted successfully";

            return deleteEntityResponse;

        }






    }
}
