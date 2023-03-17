using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sales_mvc.Models;

namespace sales_mvc.Data
{
    public class sales_mvcContext : DbContext
    {
        public sales_mvcContext (DbContextOptions<sales_mvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;

        public DbSet<Seller> Seller { get; set; } = default!;

        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;
    }
}
