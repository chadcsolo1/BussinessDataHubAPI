using API_GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace API_GraphQL.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<SalesPerson>> GetSalesPeople([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context)
        {
            return await _context.SalesPeople.Include(x => x.BusinessEntity).Include(x => x.SalesOrderHeaders).ToListAsync();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<SalesPerson> GetSalesPerson([Service(ServiceKind.Synchronized)] AdventureWorks2017Context _context, int Id)
        {
            return await _context.SalesPeople.Include(x => x.SalesPersonQuotaHistories).Include(x => x.SalesOrderHeaders)
                        .Where(x => x.BusinessEntityId == Id).FirstOrDefaultAsync();
        }
    }
}
