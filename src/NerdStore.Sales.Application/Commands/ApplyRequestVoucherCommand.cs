using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class ApplyRequestVoucherCommand : Command
{
    public Guid ClientId { get; private set; }
    public string VoucherCode { get; private set; }

    public ApplyRequestVoucherCommand(Guid clientId, string voucherCode)
    {
        ClientId = clientId;
        VoucherCode = voucherCode;

    }

    public override bool IsValid()
    {
        ValidationResult = new ApplyRequestVoucherValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ApplyRequestVoucherValidation : AbstractValidator<ApplyRequestVoucherCommand>
{
    public ApplyRequestVoucherValidation()
    {
        RuleFor(r => r.ClientId).NotEqual(Guid.Empty).WithMessage("Invalid Client ID");
        RuleFor(r => r.VoucherCode).NotEmpty().WithMessage("Invalid Voucher Code");
    }
}