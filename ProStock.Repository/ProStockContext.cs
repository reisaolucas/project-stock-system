using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository
{
    public class ProStockContext : DbContext
    {
        public ProStockContext(DbContextOptions<ProStockContext> options) : base (options) {}        
        public DbSet<Product> Products { get; set; }
    }
}