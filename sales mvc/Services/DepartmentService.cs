using Microsoft.EntityFrameworkCore;
using sales_mvc.Data;
using sales_mvc.Models;

namespace sales_mvc.Services {
    public class DepartmentService {

        private readonly sales_mvcContext _context;

        public DepartmentService(sales_mvcContext context) {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
