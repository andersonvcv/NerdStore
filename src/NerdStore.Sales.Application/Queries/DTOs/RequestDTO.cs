namespace NerdStore.Sales.Application.Queries.DTOs;

public class RequestDTO
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public decimal Value { get; set; }
    public DateTime EntryDate { get; set; }
    public int Status { get; set; }
}