using Microsoft.EntityFrameworkCore;
using sales_mvc.Data;
using sales_mvc.Models;
using sales_mvc.Services.Exeptions;

namespace sales_mvc.Services {
    public class SellerService {
        private readonly sales_mvcContext _context;

        public SellerService(sales_mvcContext context) {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller) {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int? id) {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id) {
            var seller = await _context.Seller.FindAsync(id);

            try {

                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();

            } catch (DbUpdateException e) {

                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj) {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny) {
                throw new NotFoundExeption("Id not Found");
            }

            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException e) {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
