using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using theatreers.company.Controllers;
using theatreers.shared.Interfaces;
using theatreers.shared.Models;
using theatreers.shared.Services;
using Xunit;

namespace theatreers.company.tests
{
    public class CompaniesControllerTest
    {
        CompaniesController _controller;
        ICompanyService _service;

        public CompaniesControllerTest()
        {
            _service = new CompanyServiceFake();
            _controller = new CompaniesController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Company>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(12837);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.Get(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<Company>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Company).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Company()
            {
                Id = 1
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Company testItem = new Company()
            {
                Name = "Les Miserables"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Company()
            {
                Name = "HAODS"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Company;

            // Assert
            Assert.IsType<Company>(item);
            Assert.Equal(testItem.Name, item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            int notExistingId = 13674;

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            int existingId = 1;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            int existingId = 1;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            var validateCount = _controller.Get().Result as OkObjectResult;
            var updatedItems = Assert.IsType<List<Company>>(validateCount.Value);
            Assert.Equal(2, updatedItems.Count);
        }

        [Fact]
        public void Update_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {            
            // Arrange
            var proposedUpdate = new Company()
            {
                Id = 13674,
                Name = "Wokingham Theatre"
            };

            // Act
            var badResponse = _controller.Put(proposedUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var proposedUpdate = new Company()
            {
                Id = 1,
                Name = "Reading Operatic Society"
            };

            // Act            
            var okResult = _controller.Put(proposedUpdate).Result as OkObjectResult;
            
            // Assert
            var validateResult = _controller.Get(proposedUpdate.Id).Result as OkObjectResult;
            Assert.IsType<Company>(okResult.Value);
            Assert.Equal(proposedUpdate.Name, (okResult.Value as Company).Name);
        }
    }
}
