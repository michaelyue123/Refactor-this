using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Products products;

        [HttpGet]
        public Products Get()
        {
            Products products = new();
            if (products == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return products;
        }
    }
}