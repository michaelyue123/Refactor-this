using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactorThis.Models.Repository
{
    public interface IProductOptionRepository
    {
        Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);
        Task<ProductOption> GetProductOption(Guid productId, Guid optionId);
        Task<ProductOption> AddProductOption(Guid productId, ProductOption option);
        Task<ProductOption> UpdateProductOption(ProductOption productOption);
        void DeleteProductOption(Guid productId);
    }
}
