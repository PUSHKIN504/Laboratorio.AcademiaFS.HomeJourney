using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Controllers.Generals;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using AutoMapper;
using AcademiaFS.HomeJourney.WebAPI._Features;
using AcademiaFS.HomeJourney.WebAPI.Utilities;

namespace AcademiaFS.HomeJourney.WebAPI.Tests.Integration.Controllers
{
    public class CargosControllerIntegrationTests : IDisposable
    {
        private readonly HomeJourneyContext _context;
        private readonly CargosController _controller;
        private readonly IGenericServiceInterface<Cargos, int> _cargoServiceSubstitute;
        private readonly IUnitOfWork _unitOfWorkSubstitute;
        private readonly IMapper _mapperSubstitute;

        public CargosControllerIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<HomeJourneyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new HomeJourneyContext(options);
            SeedTestData();

            _cargoServiceSubstitute = Substitute.For<IGenericServiceInterface<Cargos, int>>();
            _unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
            _mapperSubstitute = Substitute.For<IMapper>();

            _mapperSubstitute.Map<CargoDto>(Arg.Any<Cargos>())
                .Returns(callInfo => new CargoDto
                {
                    CargoId = callInfo.Arg<Cargos>().CargoId,
                    Nombre = callInfo.Arg<Cargos>().Nombre,
                });

            _mapperSubstitute.Map<Cargos>(Arg.Any<CargoDto>())
                .Returns(callInfo => new Cargos
                {
                    CargoId = callInfo.Arg<CargoDto>().CargoId,
                    Nombre = callInfo.Arg<CargoDto>().Nombre,
                });

            _mapperSubstitute.Map<List<CargoDto>>(Arg.Any<IEnumerable<Cargos>>())
                .Returns(callInfo => callInfo.Arg<IEnumerable<Cargos>>()
                    .Select(c => new CargoDto { CargoId = c.CargoId, Nombre = c.Nombre })
                    .ToList());

            _controller = new CargosController(_cargoServiceSubstitute, _unitOfWorkSubstitute, _mapperSubstitute);
        }

        private void SeedTestData()
        {
            var cargos = new List<Cargos>
            {
                new Cargos { CargoId = 1, Nombre = "Cargo 1", Activo = true },
                new Cargos { CargoId = 2, Nombre = "Cargo 2", Activo = false }
            };

            _context.Cargos.AddRange(cargos);
            _context.SaveChanges();
        }

        [Fact]
        public void GetById_ExistingId_ReturnsCargo()
        {
            // Arrange
            var cargo = _context.Cargos.First(c => c.CargoId == 1);
            _cargoServiceSubstitute.GetById(1).Returns(cargo);

            // Act
            var actionResult = _controller.GetById(1);

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>("porque la respuesta exitosa se envuelve en Ok(...)");
            var okResult = actionResult.Result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque debe existir un CustomResponse");
            response!.Success.Should().BeTrue("porque se obtuvo el objeto con éxito");
            response.Message.Should().Be("Ciudad encontrada");
            response.Data.Should().NotBeNull();
            response.Data!.CargoId.Should().Be(1);
            response.Data.Nombre.Should().Be("Cargo 1");
        }


        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _cargoServiceSubstitute.GetById(999).Returns((Cargos)null);

            // Act
            var actionResult = _controller.GetById(999);

            // Assert
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>(
                "porque un ID inexistente debe retornar NotFound"
            );

            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull();
            response!.Success.Should().BeFalse("porque la petición debe fallar con un ID inexistente");
            response.Message.Should().Be("No se encontró la ciudad con ID 999");
            response.Data.Should().BeNull("porque no existen datos con un ID inexistente");
        }


        [Fact]
        public void Create_ValidDto_ReturnsCreated()
        {
            // Arrange
            var dto = new CargoDto { CargoId = 3, Nombre = "Cargo 3" };
            var entity = new Cargos { CargoId = 3, Nombre = "Cargo 3" };
            _cargoServiceSubstitute.Create(Arg.Any<Cargos>()).Returns(entity);

            // Act
            var actionResult = _controller.Create(dto);

            // Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>(
                "porque un objeto creado correctamente debería retornar 201 Created"
            );

            var createdResult = actionResult.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();

            var response = createdResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque debe venir un CustomResponse en el body de la respuesta");
            response!.Success.Should().BeTrue("porque la creación fue exitosa");
            response.Message.Should().Be("Ciudad creada correctamente");
            response.Data.Should().NotBeNull("porque el objeto creado debe devolverse");
            response.Data!.CargoId.Should().Be(3);
            response.Data.Nombre.Should().Be("Cargo 3");

            createdResult.ActionName.Should().Be("GetById");
            createdResult.RouteValues.Should().ContainKey("id");
            createdResult.RouteValues["id"].Should().Be(3);
        }


        [Fact]
        public void Update_ValidDto_ReturnsOk()
        {
            // Arrange
            var existingCargo = new Cargos { CargoId = 1, Nombre = "Cargo 1", Activo = true };

            if (!_context.Cargos.Any(c => c.CargoId == 1))
            {
                _context.Cargos.Add(existingCargo);
                _context.SaveChanges();
            }

            var dto = new CargoDto { CargoId = 1, Nombre = "Cargo Actualizado" };

            _cargoServiceSubstitute.GetById(1).Returns(
                _context.Cargos.FirstOrDefault(c => c.CargoId == 1) 
            );

            _cargoServiceSubstitute.When(x => x.Update(Arg.Any<Cargos>())).Do(callInfo =>
            {
                var updatedCargo = callInfo.Arg<Cargos>();

                var existingCargo = _context.Cargos.FirstOrDefault(c => c.CargoId == updatedCargo.CargoId);
                if (existingCargo != null)
                {
                    existingCargo.Nombre = updatedCargo.Nombre;
                    existingCargo.Activo = updatedCargo.Activo;

                    _context.Cargos.Update(existingCargo); 
                    _context.SaveChanges();

                    _context.Entry(existingCargo).Reload(); 
                }
            });

            // Act
            var actionResult = _controller.Update(1, dto);

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>(
                "porque la actualización exitosa debe retornar 200 OK"
            );

            var okResult = actionResult.Result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull();
            response!.Success.Should().BeTrue("porque la actualización debe ser exitosa");
            response.Message.Should().Be("Cargo actualizado correctamente");
            response.Data.Should().NotBeNull();
            response.Data!.CargoId.Should().Be(1);

            var updatedCargoInDb = _context.Cargos.FirstOrDefault(c => c.CargoId == 1);
            updatedCargoInDb.Should().NotBeNull();
        }



        [Fact]
        public void Update_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var dto = new CargoDto { CargoId = 2, Nombre = "Cargo Actualizado" };

            // Act
            var actionResult = _controller.Update(1, dto);

            // Assert
            actionResult.Result.Should().BeOfType<BadRequestObjectResult>(
                "porque un ID que no coincide debería retornar BadRequest"
            );
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque en el body debería venir un CustomResponse con el error");
            response!.Success.Should().BeFalse("porque la petición debe fallar");
            response.Message.Should().Be("El ID de la ruta no coincide con el ID del objeto");
            response.Data.Should().BeNull("porque un BadRequest no devuelve datos");
        }


        [Fact]
        public void Update_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var dto = new CargoDto { CargoId = 999, Nombre = "Cargo Actualizado" };
            _cargoServiceSubstitute.GetById(999).Returns((Cargos)null);

            // Act
            var actionResult = _controller.Update(999, dto);

            // Assert
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>(
                "porque un ID inexistente debe retornar 404 Not Found"
            );
            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque en el body se debe incluir la información del error");
            response!.Success.Should().BeFalse("porque la operación falló al no encontrar el ID");
            response.Message.Should().Be("No se encontró el Cargo con ID 999");
            response.Data.Should().BeNull("porque no hay datos cuando no se encuentra el registro");
        }


        [Fact]
        public void SetActive_ExistingId_ActivatesCargo()
        {
            // Arrange
            var cargo = new Cargos { CargoId = 1, Nombre = "Cargo 1", Activo = false };
            _cargoServiceSubstitute.GetById(1).Returns(cargo);
            _cargoServiceSubstitute.When(x => x.SetActive(1, true))
                .Do(callInfo => cargo.Activo = true);

            // Act
            var actionResult = _controller.SetActive(1, true);

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>(
                "porque la acción exitosa debería retornar 200 OK"
            );

            var okResult = actionResult.Result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque debe existir un CustomResponse en la respuesta");
            response!.Success.Should().BeTrue("porque la petición debe ser exitosa");
            response.Message.Should().Be("La ciudad ha sido activada");
            response.Data.Should().NotBeNull("porque el método debe devolver el elemento actualizado");
            response.Data!.CargoId.Should().Be(1);
        }


        [Fact]
        public void SetActive_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _cargoServiceSubstitute.GetById(999).Returns((Cargos)null);

            // Act
            var actionResult = _controller.SetActive(999, true);

            // Assert
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>(
                "porque un ID inexistente debe retornar 404 Not Found"
            );

            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as CustomResponse<CargoDto>;
            response.Should().NotBeNull("porque en el body se debe incluir un CustomResponse con el error");
            response!.Success.Should().BeFalse("porque la operación debe fallar al no encontrar el ID");
            response.Message.Should().Be("No se encontró la ciudad con ID 999");
            response.Data.Should().BeNull("porque no hay datos cuando el ID no existe");
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}