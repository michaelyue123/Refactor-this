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
    public class ProductOptionController : ControllerBase
    {
        private readonly IProductOptionRepository productOptionRepository;

        public ProductOptionController(IProductOptionRepository productOptionRepository)
        {
            this.productOptionRepository = productOptionRepository;
        }

        [HttpGet("{productId}/options/{optionId}")]
        public async Task<ActionResult<ProductOption>> GetOption(Guid productId, Guid optionId)
        {
            try
            {
                var result = await productOptionRepository.GetProductOption(productId, optionId);

                //if (result.IsNew) throw new Exception();

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("{productId}/options")]
        public async Task<ActionResult<ProductOption>> CreateOption(Guid productId, ProductOption option)
        {
            try
            {
                if (option == null)
                    return BadRequest();

                // Add custom model validation error
                //var prodOpt = productOptionRepository.GetProductOption(productId, option.Id);

                //if (prodOpt != null)
                //{
                //    ModelState.AddModelError("product option id", "Product option already exists.");
                //    return BadRequest(ModelState);
                //}

                var createdProductOption = await productOptionRepository.AddProductOption(productId, option);

                return CreatedAtAction(nameof(GetOption),
                    new { id = createdProductOption.Id }, createdProductOption);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }



        [HttpPut("{productId}/options/{optionId}")]
        public async Task<ActionResult<ProductOption>> UpdateOption(Guid optionId, ProductOption option)
        {
            try
            {
                if (optionId != option.Id)
                    return BadRequest("Product Option ID mismatch");

                var optionToUpdate = await productOptionRepository.GetProductOption(option.ProductId, optionId);

                if (optionToUpdate == null)
                    return NotFound($"Product option with Id = {optionId} not found");

                return await productOptionRepository.UpdateProductOption(optionId,option);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{productId}/options/{optionId}")]
        public async Task<ActionResult<ProductOption>> DeleteOption(Guid productId, Guid optionId)
        {
            try
            {
                var optionToDelete = await productOptionRepository.GetProductOption(productId, optionId);

                if (optionToDelete == null)
                {
                    return NotFound($"Product option with Id = {optionId} not found");
                }

                return await productOptionRepository.DeleteProductOption(optionId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
