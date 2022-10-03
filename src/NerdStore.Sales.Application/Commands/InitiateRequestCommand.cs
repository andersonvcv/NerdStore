using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class InitiateRequestCommand : Command
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal Total { get; private set; }
    public string CardName { get; private set; }
    public string CardNumber { get; private set; }
    public string CardExpirationDate { get; private set; }
    public string CardCVV { get; private set; }

    public InitiateRequestCommand(Guid requestId, Guid clientId, decimal total, string cardName, string cardNumber, string cardExpirationDate, string cardCvv)
    {
        RequestId = requestId;
        ClientId = clientId;
        Total = total;
        CardName = cardName;
        CardNumber = cardNumber;
        CardExpirationDate = cardExpirationDate;
        CardCVV = cardCvv; 
    }

    public override bool IsValid()
    {
        ValidationResult = new InitiateRequestValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class InitiateRequestValidation : AbstractValidator<InitiateRequestCommand>
{
    public InitiateRequestValidation()
    {
        RuleFor(c => c.ClientId).NotEqual(Guid.Empty).WithMessage("Invalid Client Id");
        RuleFor(c => c.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid Product Id");
        RuleFor(c => c.CardName).NotEmpty().WithMessage("Card name not informed");
        RuleFor(c => c.CardNumber).CreditCard().WithMessage("Invalid card number");
        RuleFor(c => c.CardExpirationDate).NotEmpty().WithMessage("Expiration date not informed");
        RuleFor(c => c.CardCVV).Length(3,4).WithMessage("Invalid CVV");
    }
}