using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class RequestCommandHandler : IRequestHandler<AddRequestItemCommand, bool>
{
    public async Task<bool> Handle(AddRequestItemCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        return true;
    }

    private bool ValidateCommand(Command command)
    {
        if (command.Valid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
                
        }

        return false;
    }
}
