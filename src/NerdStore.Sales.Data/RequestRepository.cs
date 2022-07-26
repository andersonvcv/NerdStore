using NerdStore.Core.Data;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data;

public class RequestRepository : IRequestRepository
{
    public IUnitOfWork UnitOfWork { get; }
    public async Task<IEnumerable<Request>> GetByClientId(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public async Task<Request> GetDraftRequestByClientId(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public void Add(Request request)
    {
        throw new NotImplementedException();
    }

    public void Update(Request request)
    {
        throw new NotImplementedException();
    }

    public async Task<RequestItem> GetById(Guid requestId)
    {
        throw new NotImplementedException();
    }

    public async Task<RequestItem> GetByRequest(Guid requestId, Guid productId)
    {
        throw new NotImplementedException();
    }

    public void AddItem(RequestItem requestItem)
    {
        throw new NotImplementedException();
    }

    public void UpdateItem(RequestItem requestItem)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(RequestItem requestItem)
    {
        throw new NotImplementedException();
    }

    public async Task<Voucher> GetVoucherByCode(string code)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}