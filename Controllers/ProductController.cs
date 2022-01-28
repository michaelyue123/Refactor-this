using System;
using System.Net;
using System.Net.Http;
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
        readonly ProductRepo _productRepo = new();

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            Product product = _productRepo.Get(id);

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
            _productRepo.Save(product);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, Product product)
        {
            Product prod = _productRepo.Get(id);

            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            // update product details
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.DeliveryPrice = product.DeliveryPrice;
            

            if (!prod.IsNew)
                _productRepo.Save(prod);
            else
                throw new Exception();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Product product = _productRepo.Get(id);

            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            _productRepo.Delete(id);
        }
    }
}
