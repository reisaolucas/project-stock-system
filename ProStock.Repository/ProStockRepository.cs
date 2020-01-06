using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository
{
    public class ProStockRepository : IProStockRepository
    {
        private readonly ProStockContext _context;
        public ProStockRepository(ProStockContext context)
        {
            _context = context;

            //I'm doing this because, everytime a change is traceable, I don't want the tracking environment to be traceable
            //By doing this, I won't lock the resource at the entityframework
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //Generic add
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }
        //Generic update
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        //Generic delete
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Product[]> GetAllProductsAsync()
        {
            IQueryable<Product> query = _context.Products;
            query = query.OrderBy(product => product.Id);
            return await query.ToArrayAsync();

        }
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            IQueryable<Product> query = _context.Products;
            query = query.Where(product => product.Id == Id);
            return await query.FirstOrDefaultAsync();
        }


    }
}