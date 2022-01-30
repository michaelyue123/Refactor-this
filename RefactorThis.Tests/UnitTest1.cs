using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefactorThis.Controllers;
using RefactorThis.Models;
using RefactorThis.Models.Repository;

namespace RefactorThis.Tests
{
    [TestClass]
    public class ProductControllerUnitTests
    {
        [TestMethod]
        public void GetReturnsProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            Guid id = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3");
            mockRepository.Setup(x => x.GetProduct(id))
                .ReturnsAsync(new Product { Id = id });

            var controller = new ProductController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = (IHttpActionResult)controller.GetProduct(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsProductNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            Guid id = Guid.Parse("3d4de31e-260c-4e3b-ad49-be1e659d0374");

            // Act
            IHttpActionResult actionResult = (IHttpActionResult)controller.GetProduct(id);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostReturnsProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            // Act
            Guid id = Guid.Parse("175a8446-13d1-4348-8d25-fca472bfd91f");
            IHttpActionResult actionResult = (IHttpActionResult)controller.CreateProduct(
                new Product
                {
                    Id= id,
                    Name= "Xiao Mi 7 Plus",
                    Description= "Newest mobile product from Xiao Mi.",
                    Price= 999.99M,
                    DeliveryPrice= 15.99M
                });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("api/products", createdResult.RouteName);
            Assert.AreEqual(id, createdResult.RouteValues["id"]);
        }


        [TestMethod]
        public void PutReturnsProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            // Act
            Guid id = Guid.Parse("175a8446-13d1-4348-8d25-fca472bfd91f");
            IHttpActionResult actionResult = (IHttpActionResult)controller.UpdateProduct(id,
                new Product
                {
                    Id = id,
                    Name = "Xiao Mi 7 Plus",
                    Description = "Newest mobile product from Xiao Mi.",
                    Price = 990.99M,
                    DeliveryPrice = 12.99M
                });
            var contentResult = actionResult as NegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.Id);
        }


        [TestMethod]
        public void DeleteReturnsProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            // Act
            Guid id = Guid.Parse("175a8446-13d1-4348-8d25-fca472bfd91f");
            IHttpActionResult actionResult = (IHttpActionResult)controller.DeleteProduct(id);
            var contentResult = actionResult as NegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.Id);
        }
    }
}