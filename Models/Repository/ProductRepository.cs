﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RefactorThis.Data;

namespace RefactorThis.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // gets all products
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await appDbContext.Products.ToListAsync();
        }

        // gets all products
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await appDbContext.Products.Where(e => e.Name == name).ToListAsync();
        }

        // gets the project that matches the specified ID
        public async Task<Product> GetProduct(Guid productId)
        {
            return await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Id == productId);
        }

        // adds a new product
        public async Task<Product> AddProduct(Product product)
        {
            var result = await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // updates a product
        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Id == product.Id);

            if (result != null)
            {
                result.Name = product.Name;
                result.Description = product.Description;
                result.Price = product.Price;
                result.DeliveryPrice = product.DeliveryPrice;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        // deletes a product and its options
        public async void DeleteProduct(Guid productId)
        {
            // delete product option associated with a specific product id
            await appDbContext.ProductOptions.ForEachAsync(async e =>
            {
                if(e != null && e.ProductId == productId)
                {
                    appDbContext.ProductOptions.Remove(e);
                    await appDbContext.SaveChangesAsync();
                }
            });

            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Id == productId);

            if (result != null)
            {
                appDbContext.Products.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}