using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Commands;

public class RequestCommandHandler : IRequestHandler<AddRequestItemCommand, bool>
{
    private readonly IRequestRepository _requestRepository;

    public RequestCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<bool> Handle(AddRequestItemCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        var request = await _requestRepository.GetDraftRequestByClientId(command.ClientId);
        var requestItem = new RequestItem(command.ProductId, command.ProductName, command.Quantity, command.Value);

        if (request is null)
        {
            request = Request.RquestFactory.DraftRequest(command.ClientId);
            request.AddItem(requestItem);
            _requestRepository.Add(request);
        }
        else
        {
            request.AddItem(requestItem);
            if (request.HasRequestItem(requestItem))
            {
                _requestRepository.UpdateItem(request.RequestItems.FirstOrDefault(ri => ri.ProductId == requestItem.ProductId));
            }
            else
            {
                _requestRepository.AddItem(requestItem);
            }
        }

        return await _requestRepository.UnitOfWork.Commit();
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
