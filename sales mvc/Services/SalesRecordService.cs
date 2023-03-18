using Microsoft.EntityFrameworkCore;
using sales_mvc.Data;
using sales_mvc.Models;

namespace sales_mvc.Services {
    public class SalesRecordService {
        private readonly sales_mvcContext _context;

        public SalesRecordService(sales_mvcContext context) {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {
            var result = _context.SalesRecord.Select(x => x);

            if(minDate.HasValue) {
                result = result.Where(x => x.Date >= minDate);
            }

            if(maxDate.HasValue) {
                result = result.Where(x => x.Date <= maxDate);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) {
            var result = _context.SalesRecord.Select(x => x);

            if (minDate.HasValue) {
                result = result.Where(x => x.Date >= minDate);
            }

            if (maxDate.HasValue) {
                result = result.Where(x => x.Date <= maxDate);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }

    }
}
