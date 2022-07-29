using NerdStore.Sales.Application.Queries.DTOs;

namespace NerdStore.Sales.Application.Queries;

public interface IRequestQueries
{
    Task<CartDTO> GetClientCart(Guid clientId);

    Task<IEnumerable<RequestDTO>> GetClientRequests(Guid clientId);
}