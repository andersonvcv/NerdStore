using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class AddRequestItemCommand: Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal Value { get; private set; }

    public AddRequestItemCommand(Guid clientId, Guid productId, string productName, int quantity, decimal value)
    {
        ClientId = clientId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Value = value;
    }

    public override bool Valid()
    {
        ValidationResult = new AddItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AddItemValidation : AbstractValidator<AddRequestItemCommand>
{
    public AddItemValidation()
    {
        RuleFor(c => c.ClientId).NotEqual(Guid.Empty).WithMessage("Invalid ClientId");
        RuleFor(c => c.ProductId).NotEqual(Guid.Empty).WithMessage("Invalid ProductId");
        RuleFor(c => c.ProductName).NotEmpty().WithMessage("Invalid product name");
        RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Minimum quantity is 1");
        RuleFor(c => c.Value).GreaterThan(0).WithMessage("Value must be greater than 0");
    }
}