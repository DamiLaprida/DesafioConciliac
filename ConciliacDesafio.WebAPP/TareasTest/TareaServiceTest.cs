using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ConciliacDesafio.Domain.Contracts.Repository;
using AutoMapper;
using ConciliacDesafio.Domain.Services;
using ConciliacDesafio.Domain.Dtos;
using ConciliacDesafio.Domain.Contracts.Services;
using ConciliacDesafio.WebAPP.Controllers;
using Microsoft.AspNetCore.Mvc;
using ConciliacDesafio.Domain.Entities;

namespace ConciliacDesafio.Tests
{
#nullable disable
    public class TareaServiceTests
    {
        [Fact]
        public async Task GetAllTareasAsync_UnitTest_Success()
        {
            // Arrange
            var mockRepository = new Mock<ITareaRepository>();
            var mockMapper = new Mock<IMapper>(); 
            var tareaService = new TareaService(mockRepository.Object);
            var expectedTareas = new List<TareaDTO>
            {
                new TareaDTO {  Titulo = "Tarea 1", Descripcion = "Descripción 1" },
                new TareaDTO {  Titulo = "Tarea 2", Descripcion = "Descripción 2" }
            };

            mockRepository.Setup(repo => repo.GetAllTareasAsync())
                .ReturnsAsync(expectedTareas);

            // Act
            var result = await tareaService.GetAllTareasAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTareas, result);
        }

        [Fact]
        public async Task GetAllTareasAsync_IntegrationTest_Success()
        {
            // Arrange
            var mockRepository = new Mock<ITareaRepository>();
            var mockMapper = new Mock<IMapper>(); 
            var tareaService = new TareaService(mockRepository.Object);
            
            var tareas = new List<TareaDTO>
            {
                new TareaDTO { Titulo = "Tarea 1", Descripcion = "Descripción 1" },
                new TareaDTO { Titulo = "Tarea 2", Descripcion = "Descripción 2" }
            };

            mockRepository.Setup(repo => repo.GetAllTareasAsync())
                .ReturnsAsync(tareas);

            // Act
            var result = await tareaService.GetAllTareasAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tareas, result);
        }

        [Fact]
        public async Task GetTareaByIdAsync_WithExistingId_ShouldReturnOkResult()
        {
            // Arrange
            int existingId = 1;
            var mockService = new Mock<ITareaService>();
            var expectedTarea = new TareaDTO { Id = existingId, Titulo = "Tarea 1", Descripcion = "Descripción 1" };
            mockService.Setup(service => service.GetTareaByIdAsync(existingId))
                .ReturnsAsync(expectedTarea);
            var controller = new TareasController(mockService.Object);

            // Act
            var result = await controller.GetTareaByIdAsync(existingId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTarea = Assert.IsAssignableFrom<TareaDTO>(okResult.Value);
            Assert.Equal(expectedTarea.Id, returnedTarea.Id);
        }

        [Fact]
        public async Task GetTareaByIdAsync_WithNonExistingId_ShouldReturnNotFoundResult()
        {
            // Arrange
            int nonExistingId = 999;
            var mockService = new Mock<ITareaService>();
            mockService.Setup(service => service.GetTareaByIdAsync(nonExistingId))
                .ReturnsAsync((TareaDTO)null);
            var controller = new TareasController(mockService.Object);

            // Act
            var result = await controller.GetTareaByIdAsync(nonExistingId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No se ha encontrado la tarea solicitada.", notFoundResult.Value);
        }

        [Fact]
        public async Task EditarTareaAsync_ValidId_ReturnOk()
        {
            //Arrange
            int validId = 3;
            var mockService = new Mock<ITareaService>();
            var controller = new TareasController(mockService.Object);

            var tareaDto = new TareaDTO { Titulo = "Nuevo título", Descripcion = "Nueva descripción" };
            var tareaEditada = new TareaDTO { Id = validId, Titulo = tareaDto.Titulo, Descripcion = tareaDto.Descripcion };

            mockService.Setup(x => x.EditarTareaAsync(validId, tareaDto))
                       .ReturnsAsync(tareaEditada);

            //Act
            var response = await controller.EditarTareaAsync(validId, tareaDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var tareaEditadaResponse = Assert.IsType<TareaDTO>(okResult.Value);
            Assert.Equal(validId, tareaEditadaResponse.Id);
            Assert.Equal(tareaDto.Titulo, tareaEditadaResponse.Titulo);
            Assert.Equal(tareaDto.Descripcion, tareaEditadaResponse.Descripcion);
        }

        [Fact]
        public async Task EditarTareaAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidId = 2; // ID que devuelve null
            var mockService = new Mock<ITareaService>();
            var controller = new TareasController(mockService.Object);

            var tareaDto = new TareaDTO { Titulo = "Nuevo título", Descripcion = "Nueva descripción" };

            // Configurar el mock del servicio para devolver null cuando se llama con un ID inválido
            mockService.Setup(x => x.EditarTareaAsync(invalidId, tareaDto))
                       .ReturnsAsync((TareaDTO)null);

            // Act
            var response = await controller.EditarTareaAsync(invalidId, tareaDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task CambiarTareaPendienteACompletadaAsync_ValidId_ReturnsOk()
        {
            // Arrange
            int validId = 1; // ID válido
            var mockRepository = new Mock<ITareaRepository>();
            var mockMapper = new Mock<IMapper>();
            var tareaDTO = new TareaDTO { Id = validId, Estado = Estado.Completada };

            mockRepository.Setup(repo => repo.CambiarTareaPendienteACompletadaAsync(validId))
                          .ReturnsAsync(tareaDTO);
            mockMapper.Setup(mapper => mapper.Map<TareaDTO>(It.IsAny<TareaDTO>()))
                      .Returns(tareaDTO);

            var service = new TareaService(mockRepository.Object);
            var controller = new TareasController(service);

            // Act
            var response = await controller.CambiarTareaPendienteACompletadaAsync(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var resultValue = Assert.IsType<TareaDTO>(okResult.Value);
            Assert.Equal(validId, resultValue.Id);
            Assert.Equal(Estado.Completada, resultValue.Estado);
        }

        [Fact]
        public async Task CambiarTareaPendienteACompletadaAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidId = 2; // ID inválido
            var mockRepository = new Mock<ITareaRepository>();
            var service = new TareaService(mockRepository.Object);
            var controller = new TareasController(service);

            mockRepository.Setup(repo => repo.CambiarTareaPendienteACompletadaAsync(invalidId))
                          .ReturnsAsync((TareaDTO)null);

            // Act
            var response = await controller.CambiarTareaPendienteACompletadaAsync(invalidId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task CrearTareaAsync_Success_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<ITareaService>();
            var controller = new TareasController(mockService.Object);
            var tareaDto = new TareaDTO { Titulo = "Titulo1", Descripcion = "Descripción 1" };

            mockService.Setup(service => service.CrearTareaAsync(It.IsAny<TareaDTO>()))
                       .ReturnsAsync(tareaDto);

            // Act
            var result = await controller.CrearTareaAsync(tareaDto);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tareaDto, createdResult.Value);
        }

        [Fact]
        public async Task CrearTareaAsync_CreatedSinTituloODescripcion_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<ITareaService>();
            var controller = new TareasController(mockService.Object);
            var tareaDto = new TareaDTO { };

            mockService.Setup(service => service.CrearTareaAsync(It.IsAny<TareaDTO>()))
                       .ReturnsAsync((TareaDTO)null);

            // Act
            var result = await controller.CrearTareaAsync(tareaDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task BorrarTareaAsync_ValidId_ReturnsOk()
        {
            // Arrange
            int validId = 1;
            var mockService = new Mock<ITareaService>();
            mockService.Setup(service => service.BorrarTareaAsync(validId))
                       .ReturnsAsync(new TareaDTO { Id = validId }); // Simula una tarea borrada con éxito
            var controller = new TareasController(mockService.Object);

            // Act
            var response = await controller.BorrarTareaAsync(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var tareaBorrada = Assert.IsType<TareaDTO>(okResult.Value);
            Assert.Equal(validId, tareaBorrada.Id);
        }

        [Fact]
        public async Task BorrarTareaAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidId = 2;
            var mockService = new Mock<ITareaService>();
            mockService.Setup(service => service.BorrarTareaAsync(invalidId))
                       .ReturnsAsync((TareaDTO)null); // Simula una tarea no encontrada
            var controller = new TareasController(mockService.Object);

            // Act
            var response = await controller.BorrarTareaAsync(invalidId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
