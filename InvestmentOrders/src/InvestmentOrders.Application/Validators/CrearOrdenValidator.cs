using FluentValidation;
using InvestmentOrders.Application.DTOs;

namespace InvestmentOrders.Application.Validators;

public class CrearOrdenValidator : AbstractValidator<CrearOrdenDto>
{
    public CrearOrdenValidator()
    {
        RuleFor(x => x.IdCuenta).NotEmpty();
        RuleFor(x => x.NombreActivo)
            .NotEmpty()
            .MaximumLength(32);
        RuleFor(x => x.Cantidad)
            .GreaterThan(0);
        RuleFor(x => x.Operacion)
            .Must(op => op == 'C' || op == 'V')
            .WithMessage("La operación debe ser 'C' (Compra) o 'V' (Venta)");
    }
}