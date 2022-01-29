using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductOptionsController : ControllerBase
    {
        private readonly IProductOptionRepository productOptionRepository;

        public ProductOptionsController(IProductOptionRepository productOptionRepository)
        {
            this.productOptionRepository = productOptionRepository;
        }

        [HttpGet("{productId}/options")]
        public async Task<ActionResult<IEnumerable<ProductOption>>> GetOptions(Guid productId)
        {
            try
            {
                return (await productOptionRepository.GetProductOptions(productId)).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
