using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactorThis.Models.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<Product> GetProduct(Guid id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(Guid id);
    }
}
