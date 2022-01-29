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
    public class ProductOptionController : ControllerBase
    {
        [HttpGet("{productId}/options/{id}")]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            ProductOption option = new(id);

            if (option == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (option.ProductId != productId)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product option with ID = {0}", productId)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (option.IsNew)
                throw new Exception();

            return option;
        }

        [HttpPost("{productId}/options")]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [HttpPut("{productId}/options/{id}")]
        public void UpdateOption(Guid id, ProductOption option)
        {
            ProductOption opt = new(id)
            {
                Name = option.Name,
                Description = option.Description
            };

            if (opt == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (!opt.IsNew)
                opt.Save();
            else
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{productId}/options/{id}")]
        public void DeleteOption(Guid id)
        {
            ProductOption opt = new(id);

            if (opt == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product option with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            opt.Delete();
        }
    }
}
