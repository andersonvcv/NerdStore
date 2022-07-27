using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain;

public interface IRequestRepository: IRepository<Request>
{
    Task<IEnumerable<Request>> GetByClientId(Guid clientId);
    Task<Request> GetDraftRequestByClientId(Guid clientId);
    Task<Request> GetById(Guid requestId);
    void Add(Request request);
    void Update(Request request);

    Task<RequestItem> GetByRequest(Guid requestId, Guid productId);
    void AddItem(RequestItem requestItem);
    void UpdateItem(RequestItem requestItem);
    void RemoveItem(RequestItem requestItem);

    Task<Voucher> GetVoucherByCode(string code);
}