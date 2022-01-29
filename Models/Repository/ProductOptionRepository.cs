using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Data;

namespace RefactorThis.Models.Repository
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductOptionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // gets all options for a specified product id
        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            return await appDbContext.ProductOptions.Where(e => e.ProductId == productId).ToListAsync();
        }

        // gets the specified product option for the specified product.
        public async Task<ProductOption> GetProductOption(Guid productId, Guid optionId)
        {
            return await appDbContext.ProductOptions.
                FirstOrDefaultAsync(e => e.Id == optionId && e.ProductId == productId);
        }

        // adds a new product option to the specified product
        public async Task<ProductOption> AddProductOption(Guid productId, ProductOption option)
        {
            var result = option.ProductId == productId ?
                await appDbContext.ProductOptions.AddAsync(option) : null;

            if(result != null)
            {
                await appDbContext.SaveChangesAsync();
            }
            return result.Entity;
        }

        // updates the specified product option
        public async Task<ProductOption> UpdateProductOption(Guid optionId, ProductOption option)
        {
            var result = await appDbContext.ProductOptions
                .FirstOrDefaultAsync(e => e.Id == optionId);

            if (result != null)
            {
                result.Name = option.Name;
                result.Description = option.Description;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        // deletes the specified product option
        public async Task<ProductOption> DeleteProductOption(Guid optionId)
        {
            var result = await appDbContext.ProductOptions
                .FirstOrDefaultAsync(e => e.Id == optionId);

            if (result != null)
            {
                appDbContext.ProductOptions.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
