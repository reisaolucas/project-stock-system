using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository
{
    //In the future, if any developer wants to add a new Repository, this interface can be used
    public interface IProStockRepository
    {
        //General
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //Products
         Task<Product[]> GetAllProductsAsync();
         Task<Product> GetProductByIdAsync(int Id);
         
    }
}