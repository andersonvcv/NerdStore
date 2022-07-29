using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Commands;

public class RequestCommandHandler : IRequestHandler<AddRequestItemCommand, bool>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMediatoRHandler _mediatoRHandler;

    public RequestCommandHandler(IRequestRepository requestRepository, IMediatoRHandler mediatoRHandler)
    {
        _requestRepository = requestRepository;
        _mediatoRHandler = mediatoRHandler;
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
            request.AddEvent(new DraftRequestEvent(command.ClientId, request.Id));
        }
        else
        {
            var hasItem = request.HasRequestItem(requestItem);
            request.AddItem(requestItem);
            if (hasItem)
            {
                _requestRepository.UpdateItem(request.RequestItems.FirstOrDefault(ri => ri.ProductId == requestItem.ProductId));
            }
            else
            {
                _requestRepository.AddItem(requestItem);
            }

            request.AddEvent(new UpdatedRequestEvent(request.ClientId, request.Id, request.Total));
        }

        request.AddEvent(new AddedRequestItemEvent(request.ClientId, request.Id, command.ProductId, command.ProductName, command.Value, command.Quantity));
        return await _requestRepository.UnitOfWork.Commit();
    }

    private bool ValidateCommand(Command command)
    {
        if (command.Valid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _mediatoRHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
        }

        return false;
    }
}
