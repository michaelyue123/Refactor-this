using System.Net;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;
using RefactorThis.Models.Repository;

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly ProductsRepo _productsRepo = new();
        // get all products
        [HttpGet]
        public Products Get()
        {
            Products products = _productsRepo.Get(null);
            return products;
        }

        // get all products matching 
        [HttpGet("{name}")]
        public Products Get(string name)
        {
            Products products = _productsRepo.Get(name);
            return products;
        }
    }
}