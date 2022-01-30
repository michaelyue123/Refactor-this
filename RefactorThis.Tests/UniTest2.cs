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
    public class ProductOptionControllerUnitTests
    {
        [TestMethod]
        public void GetReturnsProductOption()
        {
            // Arrange
            var mockRepository = new Mock<IProductOptionRepository>();
            Guid productId = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3");
            Guid optionId = Guid.Parse("5c2996ab-54ad-4999-92d2-89245682d534");

            mockRepository.Setup(x => x.GetProductOption(productId, optionId))
                .ReturnsAsync(new ProductOption { Id = optionId });

            var controller = new ProductOptionController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = (IHttpActionResult)controller.GetOption(productId, optionId);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(optionId, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsProductOptionNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IProductOptionRepository>();
            var controller = new ProductOptionController(mockRepository.Object);
            Guid productId = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafad3");
            Guid optionId = Guid.Parse("5c2996ab-54ad-4999-92d2-89245682d354");

            // Act
            IHttpActionResult actionResult = (IHttpActionResult)controller.GetOption(productId, optionId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostReturnsProductOption()
        {
            // Arrange
            var mockRepository = new Mock<IProductOptionRepository>();
            var controller = new ProductOptionController(mockRepository.Object);

            // Act
            Guid productId = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3");
            Guid optionId = Guid.Parse("747b3305-6ae8-44ac-a3b3-c103f955c2db");
            IHttpActionResult actionResult = (IHttpActionResult)controller.CreateOption(productId,
                new ProductOption
                {
                    Id = optionId,
                    ProductId = productId,
                    Name = "Black",
                    Description = "Black Apple iPhone 9S",
                });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(optionId, createdResult.RouteValues["id"]);
        }


        [TestMethod]
        public void PutReturnsProductOption()
        {
            // Arrange
            var mockRepository = new Mock<IProductOptionRepository>();
            var controller = new ProductOptionController(mockRepository.Object);

            // Act
            Guid productId = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3");
            Guid optionId = Guid.Parse("747b3305-6ae8-44ac-a3b3-c103f955c2db");
            IHttpActionResult actionResult = (IHttpActionResult)controller.UpdateOption(optionId,
                new ProductOption
                {
                    Id = optionId,
                    ProductId = productId,
                    Name = "White",
                    Description = "White Apple iPhone 9S",
                });
            var contentResult = actionResult as NegotiatedContentResult<ProductOption>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(optionId, contentResult.Content.Id);
        }

        [TestMethod]
        public void DeleteReturnsProductOption()
        {
            // Arrange
            var mockRepository = new Mock<IProductOptionRepository>();
            var controller = new ProductOptionController(mockRepository.Object);

            // Act
            Guid productId = Guid.Parse("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3");
            Guid optionId = Guid.Parse("747b3305-6ae8-44ac-a3b3-c103f955c2db");
            IHttpActionResult actionResult = (IHttpActionResult)controller.DeleteOption(productId, optionId);
            var contentResult = actionResult as NegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(optionId, contentResult.Content.Id);
        }
    }
}