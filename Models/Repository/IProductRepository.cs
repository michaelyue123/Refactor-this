using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactorThis.Models.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        void DeleteProduct(Guid productId);
    }
}
