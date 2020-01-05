using Microsoft.EntityFrameworkCore;
using ProStock.WebAPI.Model;

namespace ProStock.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}        
        public DbSet<Product> Products { get; set; }
    }
}