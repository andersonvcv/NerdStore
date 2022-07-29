using NerdStore.Sales.Application.Queries.DTOs;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Queries;

public class RequestQueries : IRequestQueries
{
    private readonly IRequestRepository _requestRepository;

    public RequestQueries(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<CartDTO> GetClientCart(Guid clientId)
    {
        var request = await _requestRepository.GetDraftRequestByClientId(clientId);
        if (request is null) return null;

        var cart = new CartDTO
        {
            ClientId = request.ClientId,
            Total = request.Total,
            RequestId = request.Id,
            Discount = request.Discount,
            SubTotal = request.Discount + request.Total
        };

        if (request.VoucherId is not null)
        {
            cart.VoucherCode = request.Voucher.Code;
        }

        foreach (var item in request.RequestItems)
        {
            cart.Items.Add(new CartItemDTO
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Value = item.Value,
                Total = item.Value * item.Quantity
            });
        }
        
        return cart;
    }

    public async Task<IEnumerable<RequestDTO>> GetClientRequests(Guid clientId)
    {
        var requests = await _requestRepository.GetByClientId(clientId);

        requests = requests.Where(r => r.Status is RequestStatus.Payed or RequestStatus.Canceled)
            .OrderByDescending(r => r.Code);

        if (!requests.Any()) return null;

        var requestDTO = new List<RequestDTO>();

        foreach (var request in requests)
        {
            requestDTO.Add(new RequestDTO
            {
                Value = request.Total,
                Status = (int)request.Status,
                Code = request.Code,
                EntryDate = request.EntryDate

            });
        }

        return requestDTO;
    }
}