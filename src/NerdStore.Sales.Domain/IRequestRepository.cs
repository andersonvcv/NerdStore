using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain;

public interface IRequestRepository: IRepository<Request>
{
    Task<IEnumerable<Request>> GetByClientId(Guid clientId);
    Task<Request> GetDraftRequestByClientId(Guid clientId);
    void Add(Request request);
    void Update(Request request);

    Task<RequestItem> GetById(Guid requestId);
    Task<RequestItem> GetByRequest(Guid requestId, Guid productId);
    void AddItem(RequestItem requestItem);
    void UpdateItem(RequestItem requestItem);
    void RemoveItem(RequestItem requestItem);

    Task<Voucher> GetVoucherByCode(string code);
}