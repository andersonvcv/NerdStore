using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.IntegrationEvents;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Commands;

public class RequestCommandHandler : 
    IRequestHandler<AddRequestItemCommand, bool>,
    IRequestHandler<UpdateRequestItemCommand, bool>,
    IRequestHandler<RemoveRequestItemCommand, bool>,
    IRequestHandler<ApplyRequestVoucherCommand, bool>,
    IRequestHandler<InitiateRequestCommand, bool>,
    IRequestHandler<FinalizeRequestCommand, bool>,
    IRequestHandler<CancelRequestReplenishStockCommand, bool>,
    IRequestHandler<CancelRequestCommamnd, bool>
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

    public async Task<bool> Handle(UpdateRequestItemCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        var request = await _requestRepository.GetDraftRequestByClientId(command.ClientId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Request not found!"));
            return false;
        }

        var requestItem = await _requestRepository.GetByRequest(request.Id, command.ProductId);

        if (!request.HasRequestItem(requestItem))
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Request Item not found!"));
            return false;
        }

        request.UpdateItemQuantity(requestItem, command.Quantity);

        request.AddEvent(new UpdatedRequestEvent(request.ClientId, request.Id, request.Total));

        _requestRepository.UpdateItem(requestItem);
        _requestRepository.Update(request);

        return await _requestRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(RemoveRequestItemCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        var request = await _requestRepository.GetDraftRequestByClientId(command.ClientId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Request not found!"));
            return false;
        }

        var requestItem = await _requestRepository.GetByRequest(request.Id, command.ProductId);

        if (requestItem is not null && !request.HasRequestItem(requestItem))
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Request Item not found!"));
            return false;
        }

        request.RemoveItem(requestItem);

        request.AddEvent(new RemovedRequestItemEvent(request.ClientId, request.Id, command.ProductId));

        _requestRepository.RemoveItem(requestItem);
        _requestRepository.Update(request);

        return await _requestRepository.UnitOfWork.Commit();


    }

    public async Task<bool> Handle(ApplyRequestVoucherCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        var request = await _requestRepository.GetDraftRequestByClientId(command.ClientId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Request not found!"));
            return false;
        }

        var voucher = await _requestRepository.GetVoucherByCode(command.VoucherCode);

        if (voucher is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("request", "Voucher not found!"));
            return false;
        }

        var voucherValidation = request.ApplyVoucher(voucher);

        if (!voucherValidation.IsValid)
        {
            foreach (var error in voucherValidation.Errors)
            {
                await _mediatoRHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            return false;
        }

        request.AddEvent(new AppliedVoucherEvent(request.ClientId, request.Id, voucher.Id));
        request.AddEvent(new UpdatedRequestEvent(request.ClientId, request.Id, request.Total));

        //TODO: Subtract voucher quantity available

        _requestRepository.Update(request);

        return await _requestRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(InitiateRequestCommand command, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(command)) return false;

        var request = await _requestRepository.GetDraftRequestByClientId(command.ClientId);
        request.Initiate();

        var items = new List<Item>();
        request.RequestItems.ForEach(ri => items.Add(new Item { Id = ri.ProductId, Quantity = ri.Quantity }));
        var requestItems = new RequestItems { RequestId = request.Id, Items = items };

        request.AddEvent(new InitiatedRequestEvent(request.Id, request.ClientId, requestItems, request.Total, command.CardName, command.CardNumber, command.CardExpirationDate, command.CardCVV));

        _requestRepository.Update(request);
        return await _requestRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(FinalizeRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetById(command.RequestId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("Request", "Request not founded"));
            return false;
        }

        request.MarkAsPayed();

        request.AddEvent(new RequestFinalizedEvent(command.RequestId));
        return await _requestRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(CancelRequestReplenishStockCommand command, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetById(command.RequestId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("Request", "Request not founded"));
            return false;
        }

        var items = new List<Item>();
        request.RequestItems.ForEach(i => items.Add(new Item{Id = i.ProductId, Quantity = i.Quantity}));
        var requestItems = new RequestItems { RequestId = request.Id, Items = items };

        request.AddEvent(new CanceledRequestEvent(request.Id, request.ClientId, requestItems));
        request.MarkAsDraft();

        return await _requestRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(CancelRequestCommamnd command, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetById(command.RequestId);

        if (request is null)
        {
            await _mediatoRHandler.PublishNotification(new DomainNotification("Request", "Request not found!"));
            return false;
        }

        request.MarkAsDraft();

        return await _requestRepository.UnitOfWork.Commit();
    }

    private bool ValidateCommand(Command command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _mediatoRHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
        }

        return false;
    }
}
