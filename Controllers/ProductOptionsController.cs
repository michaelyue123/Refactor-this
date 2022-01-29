using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        [HttpGet("{productId}/options")]
        public ProductOptions GetOptions(Guid productId)
        {
            ProductOptions options = new(productId);
            if (options == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return options;
        }
    }
}
