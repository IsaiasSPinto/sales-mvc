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

        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller) {
            _context.Seller.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int? id) {
            return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id) {
            var seller = _context.Seller.Find(id);

            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller obj) {
            if(!_context.Seller.Any(x => x.Id == obj.Id)) {
                throw new NotFoundExeption("Id not Found");
            }

            try {
                _context.Update(obj);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException e) {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
