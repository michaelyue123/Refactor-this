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
        Task<ProductOption> UpdateProductOption(Guid optionId, ProductOption option);
        Task<ProductOption> DeleteProductOption(Guid optionId);
    }
}
