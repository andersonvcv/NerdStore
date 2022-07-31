using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class RemoveRequestItemCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }

    public RemoveRequestItemCommand(Guid clientId, Guid productId)
    {
        ClientId = clientId;
        ProductId = productId; 
        
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveRequestItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveRequestItemValidation : AbstractValidator<RemoveRequestItemCommand>
{
    public RemoveRequestItemValidation()
    {
        RuleFor(r => r.ClientId).NotEqual(Guid.Empty).WithMessage("Invalid Client ID");
        RuleFor(r => r.ProductId).NotEqual(Guid.Empty).WithMessage("Invalid Product ID");
    }
}