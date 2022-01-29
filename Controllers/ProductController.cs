using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefactorThis.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            Product product = new(id);

            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (product.IsNew)
                throw new Exception();

            return product;
        }

        [HttpPost]
        public void Post(Product product)
        {
            product.Save();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, Product product)
        {
            Product prod = new(id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            if (prod == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (!prod.IsNew)
                prod.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Product product = new(id);

            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            product.Delete();
        }
    }
}
