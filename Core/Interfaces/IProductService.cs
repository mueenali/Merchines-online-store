using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductService
    {
         Task<Product> GetProductAsync(int id); 
         Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}