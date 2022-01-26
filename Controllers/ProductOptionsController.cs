using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefactorThis.Controllers
{
    public class ProductOptionsController : ControllerBase
    {
        [HttpGet("{productId}/options")]
        public ProductOptions GetOptions(Guid productId)
        {
            return new ProductOptions(productId);
        }
    }
}
