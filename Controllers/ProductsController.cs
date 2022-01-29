using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public Products Get()
        {
            return new Products();
        }

        [HttpGet("GetByName/{name}")]
        public Products Get(string name)
        {
            return new Products(name);
        }
    }
}