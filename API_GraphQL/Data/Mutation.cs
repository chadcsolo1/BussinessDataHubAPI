using API_GraphQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_GraphQL.Data
{
    public class Mutation
    {
        public async Task<SalesPerson> CreateSalesPerson([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context,
            int territoryId, decimal salesQuota, decimal bonus, decimal commissionPct, decimal salesYTD, decimal salesLastYear,
            Guid rowGuid, DateTime modifiedDate)
        {
            SalesPerson newSalesPerson = new SalesPerson
            {
                TerritoryId = territoryId,
                SalesQuota = salesQuota,
                Bonus = bonus,
                CommissionPct = commissionPct,
                SalesYtd = salesYTD,
                SalesLastYear = salesLastYear,
                Rowguid = rowGuid,
                ModifiedDate = DateTime.UtcNow
            };

            _context.SalesPeople.Add(newSalesPerson);
            await _context.SaveChangesAsync();
            return newSalesPerson;
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
    }
}
