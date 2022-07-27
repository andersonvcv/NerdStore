using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data;

public class RequestRepository : IRequestRepository
{
    private readonly SalesContext _salesContext;

    public RequestRepository(SalesContext salesContext)
    {
        _salesContext = salesContext;
    }

    public IUnitOfWork UnitOfWork => _salesContext;

    public async Task<Request> GetById(Guid requestId)
    {
        return await _salesContext.Request.FindAsync(requestId);
    }

    public async Task<Request> GetDraftRequestByClientId(Guid clientId)
    {
        var request =
            await _salesContext.Request.FirstOrDefaultAsync(r =>
                r.ClientId == clientId && r.Status == RequestStatus.Draft);
        if (request is null) return null;

        await _salesContext.Entry(request).Collection(r => r.RequestItems).LoadAsync();

        if (request.HasVoucher)
        {
            await _salesContext.Entry(request).Reference(r => r.Voucher).LoadAsync();
        }

        return request;
    }

    public async Task<IEnumerable<Request>> GetByClientId(Guid clientId)
    {
        return await _salesContext.Request.AsNoTracking().Where(r => r.ClientId == clientId).ToListAsync();
    }

    public void Add(Request request)
    {
        _salesContext.Add(request);
    }

    public void Update(Request request)
    {
        _salesContext.Update(request);
    }

    public async Task<RequestItem> GetByRequest(Guid requestId, Guid productId)
    {
        return await _salesContext.RequestItem.FirstOrDefaultAsync(r => r.ProductId == productId && r.RequestId == requestId);
    }

    public void AddItem(RequestItem requestItem)
    {
        _salesContext.RequestItem.Add(requestItem);
    }

    public void UpdateItem(RequestItem requestItem)
    {
        _salesContext.RequestItem.Update(requestItem);
    }

    public void RemoveItem(RequestItem requestItem)
    {
        _salesContext.RequestItem.Remove(requestItem);
    }

    public async Task<Voucher> GetVoucherByCode(string code)
    {
        return await _salesContext.Voucher.FirstOrDefaultAsync(r => r.Code == code);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}