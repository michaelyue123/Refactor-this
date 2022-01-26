using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public Products Get()
        {
            return new Products();
        }
    }
}