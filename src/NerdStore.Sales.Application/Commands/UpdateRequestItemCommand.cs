using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class UpdateRequestItemCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UpdateRequestItemCommand(Guid clientId, Guid productId, int quantity)
    {
        ClientId = clientId;
        ProductId = productId;
        Quantity = quantity;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateRequestItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateRequestItemValidation : AbstractValidator<UpdateRequestItemCommand>
{
    public UpdateRequestItemValidation()
    {
        RuleFor(r => r.ClientId).NotEqual(Guid.Empty).WithMessage("Invalid Client ID");
        RuleFor(r => r.ProductId).NotEqual(Guid.Empty).WithMessage("Invalid Product ID");
        RuleFor(r => r.Quantity).GreaterThan(0).WithMessage("Minimum quantity is 1");
    }
}