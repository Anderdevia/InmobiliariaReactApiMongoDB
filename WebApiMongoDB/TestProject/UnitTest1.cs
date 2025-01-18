using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApiMongoDB.Controllers;
using WebApiMongoDB.Data;
using WebApiMongoDB.Entities;
using FluentAssertions;

namespace WebApiMongoDB.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private Mock<OwnerService> _mockOwnerService;
        private OwnerController _controller;

        public UnitTest1()
        {
            // Configuración del mock para el servicio OwnerService
            _mockOwnerService = new Mock<OwnerService>(null);
            _controller = new OwnerController(_mockOwnerService.Object);
        }

        [Test]
        public void GetOwners_ShouldReturnAllOwners()
        {
            // Arrange: Crear una lista de propietarios "fake"
            var fakeOwners = new List<Owner>
            {
                new Owner { IdOwner = "1", Name = "Owner One" },
                new Owner { IdOwner = "2", Name = "Owner Two" }
            };

            // Configurar el mock para que devuelva los propietarios "fake"
            _mockOwnerService.Setup(service => service.GetOwners()).Returns(fakeOwners);

            // Act: Llamar al método GetOwners del controlador
            var result = _controller.GetOwners();

            // Assert: Verificar que el resultado es OK y contiene la lista de propietarios
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result.Result;
            var returnedOwners = okResult.Value.Should().BeOfType<List<Owner>>().Subject;
            returnedOwners.Should().HaveCount(2);
        }

        [Test]
        public void GetOwner_ShouldReturnNotFound_WhenOwnerDoesNotExist()
        {
            // Arrange: Configurar el mock para devolver null cuando no se encuentra el propietario
            _mockOwnerService.Setup(service => service.GetOwner(It.IsAny<string>())).Returns((Owner)null);

            // Act: Llamar al método GetOwner del controlador con un ID inexistente
            var result = _controller.GetOwner("nonexistentId");

            // Assert: Verificar que el resultado es un NotFoundResult
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void GetOwner_ShouldReturnOkResult_WhenOwnerExists()
        {
            // Arrange: Configurar el mock para devolver un propietario "fake"
            var fakeOwner = new Owner { IdOwner = "1", Name = "Owner One" };
            _mockOwnerService.Setup(service => service.GetOwner(It.IsAny<string>())).Returns(fakeOwner);

            // Act: Llamar al método GetOwner del controlador con un ID válido
            var result = _controller.GetOwner("1");

            // Assert: Verificar que el resultado es OK y contiene la propiedad correcta
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var owner = okResult.Value.Should().BeOfType<Owner>().Subject;
            owner.IdOwner.Should().Be("1");
            owner.Name.Should().Be("Owner One");
        }

        [Test]
        public void PostOwner_ShouldReturnCreatedResult_WhenOwnerIsCreated()
        {
            // Arrange: Crear una propiedad para insertar
            var newOwner = new Owner { Name = "New Owner" };

            // Configurar el mock para devolver la propiedad creada
            _mockOwnerService.Setup(service => service.PostOwner(It.IsAny<Owner>())).Returns(newOwner);

            // Act: Llamar al método PostOwner del controlador
            var result = _controller.PostOwner(newOwner);

            // Assert: Verificar que el resultado es un CreatedAtActionResult
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var owner = createdResult.Value.Should().BeOfType<Owner>().Subject;
            owner.Name.Should().Be("New Owner");
        }

        [Test]
        public void DeleteOwner_ShouldReturnNotFound_WhenOwnerDoesNotExist()
        {
            // Arrange: Configurar el mock para devolver null cuando no se encuentra el propietario
            _mockOwnerService.Setup(service => service.DeleteOwner(It.IsAny<string>())).Returns((Owner)null);

            // Act: Llamar al método DeleteOwner del controlador con un ID inexistente
            var result = _controller.DeleteOwner("nonexistentId");

            // Assert: Verificar que el resultado es un NotFoundResult
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void DeleteOwner_ShouldReturnOk_WhenOwnerIsDeleted()
        {
            // Arrange: Configurar el mock para devolver un propietario "fake"
            var fakeOwner = new Owner { IdOwner = "1", Name = "Owner One" };
            _mockOwnerService.Setup(service => service.DeleteOwner(It.IsAny<string>())).Returns(fakeOwner);

            // Act: Llamar al método DeleteOwner del controlador con un ID válido
            var result = _controller.DeleteOwner("1");

            // Assert: Verificar que el resultado es OK y contiene el propietario eliminado
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var deletedOwner = okResult.Value.Should().BeOfType<Owner>().Subject;
            deletedOwner.IdOwner.Should().Be("1");
            deletedOwner.Name.Should().Be("Owner One");
        }
    }
}
