using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using InvestmentOrders.API.Controllers;
using InvestmentOrders.Application.DTOs;
using InvestmentOrders.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InvestmentOrders.UnitTests.Controllers;

public class OrdenesControllerTests
{
    private Mock<IOrdenService> _ordenServiceMock;
    private Mock<IValidator<CrearOrdenDto>> _validatorMock;
    private OrdenesController _controller;

    [SetUp]
    public void Setup()
    {
        _ordenServiceMock = new Mock<IOrdenService>();
        _validatorMock = new Mock<IValidator<CrearOrdenDto>>();
        _controller = new OrdenesController(_ordenServiceMock.Object, _validatorMock.Object);
    }

    [Test]
    public async Task CrearOrden_ConDatosValidos_RetornaCreated()
    {
        // Arrange
        var ordenDto = new CrearOrdenDto
        {
            IdCuenta = 1,
            NombreActivo = "Apple",
            Cantidad = 100,
            Operacion = 'C',
            ActivoId = 1
        };

        var ordenCreada = new OrdenDto
        {
            Id = 1,
            IdCuenta = ordenDto.IdCuenta,
            NombreActivo = ordenDto.NombreActivo,
            Cantidad = ordenDto.Cantidad,
            Operacion = ordenDto.Operacion,
            EstadoId = 0,
            DescripcionEstado = "En proceso"
        };

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CrearOrdenDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _ordenServiceMock.Setup(x => x.CrearOrdenAsync(It.IsAny<CrearOrdenDto>()))
            .ReturnsAsync(ordenCreada);

        // Act
        var result = await _controller.CrearOrden(ordenDto);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        createdAtActionResult.Should().NotBeNull();
        createdAtActionResult!.ActionName.Should().Be(nameof(OrdenesController.GetOrden));
        createdAtActionResult.RouteValues!["id"].Should().Be(1);
        var ordenDevuelta = createdAtActionResult.Value as OrdenDto;
        ordenDevuelta.Should().BeEquivalentTo(ordenCreada);
    }

    [Test]
    public async Task CrearOrden_ConDatosInvalidos_RetornaBadRequest()
    {
        // Arrange
        var ordenDto = new CrearOrdenDto();
        var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Cantidad", "La cantidad debe ser mayor a 0")
        };

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CrearOrdenDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _controller.CrearOrden(ordenDto);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.Value.Should().BeEquivalentTo(validationFailures);
    }

    [Test]
    public async Task GetOrden_CuandoExiste_RetornaOrden()
    {
        // Arrange
        var ordenExistente = new OrdenDto
        {
            Id = 1,
            IdCuenta = 1,
            NombreActivo = "Apple",
            Cantidad = 100
        };

        _ordenServiceMock.Setup(x => x.GetOrdenByIdAsync(1))
            .ReturnsAsync(ordenExistente);

        // Act
        var result = await _controller.GetOrden(1);

        // Assert
        result.Value.Should().BeEquivalentTo(ordenExistente);
    }

    [Test]
    public async Task GetOrden_CuandoNoExiste_RetornaNotFound()
    {
        // Arrange
        _ordenServiceMock.Setup(x => x.GetOrdenByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((OrdenDto)null);

        // Act
        var result = await _controller.GetOrden(1);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task GetOrdenes_RetornaTodasLasOrdenes()
    {
        // Arrange
        var ordenes = new List<OrdenDto>
        {
            new() { Id = 1, NombreActivo = "Apple" },
            new() { Id = 2, NombreActivo = "Microsoft" }
        };

        _ordenServiceMock.Setup(x => x.GetOrdenesAsync())
            .ReturnsAsync(ordenes);

        // Act
        var result = await _controller.GetOrdenes();

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        var ordenesDevueltas = okResult!.Value as IEnumerable<OrdenDto>;
        ordenesDevueltas.Should().BeEquivalentTo(ordenes);
    }

    [Test]
    public async Task ActualizarEstado_CuandoExiste_RetornaNoContent()
    {
        // Arrange
        _ordenServiceMock.Setup(x => x.ActualizarEstadoOrdenAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ActualizarEstado(1, 1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task ActualizarEstado_CuandoNoExiste_RetornaNotFound()
    {
        // Arrange
        _ordenServiceMock.Setup(x => x.ActualizarEstadoOrdenAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.ActualizarEstado(1, 1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task EliminarOrden_CuandoExiste_RetornaNoContent()
    {
        // Arrange
        _ordenServiceMock.Setup(x => x.EliminarOrdenAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.EliminarOrden(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task EliminarOrden_CuandoNoExiste_RetornaNotFound()
    {
        // Arrange
        _ordenServiceMock.Setup(x => x.EliminarOrdenAsync(It.IsAny<int>()))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.EliminarOrden(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}