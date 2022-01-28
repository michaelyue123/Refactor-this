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
    public class ProductOptionController : ControllerBase
    {
        readonly ProductOptionRepo _productOptionRepo = new();

        [HttpGet("{productId}/options/{id}")]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            ProductOption option = _productOptionRepo.Get(id);

            if (option == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if(option.ProductId != productId)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product option with ID = {0}", productId)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            if (option.IsNew)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            return option;
        }

        [HttpPost("{productId}/options")]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            _productOptionRepo.Save(option);
        }

        [HttpPut("{productId}/options/{id}")]
        public void UpdateOption(Guid id, ProductOption option)
        {
            ProductOption productOption = _productOptionRepo.Get(id);

            if(productOption == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product option with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            
            productOption.Name = option.Name;
            productOption.Description = option.Description;
            

            if (!productOption.IsNew)
                _productOptionRepo.Save(productOption);
        }

        [HttpDelete("{productId}/options/{id}")]
        public void DeleteOption(Guid id)
        {
            ProductOption productOption = _productOptionRepo.Get(id);

            if (productOption == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product option with ID = {0}", id)),
                    ReasonPhrase = "ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            _productOptionRepo.Delete(id);
        }
    }
}
