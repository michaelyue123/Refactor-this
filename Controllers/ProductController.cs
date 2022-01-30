using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;
using RefactorThis.Models.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            try
            {
                var result = await productRepository.GetProduct(id);

                if (result == null)
                {
                    NotFound($"Product with Id = {id} not found");
                }
                
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product does not exist");

                var createdProduct = await productRepository.AddProduct(product);

                return CreatedAtAction(nameof(GetProduct),
                    new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest("Product ID mismatch");

                var productToUpdate = await productRepository.GetProduct(id);

                if (productToUpdate == null)
                    return NotFound($"Product with Id = {id} not found");

                return await productRepository.UpdateProduct(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            try
            {
                var prodcutToDelete = await productRepository.GetProduct(id);

                if (prodcutToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                return await productRepository.DeleteProduct(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
