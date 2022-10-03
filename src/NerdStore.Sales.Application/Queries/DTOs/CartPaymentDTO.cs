namespace NerdStore.Sales.Application.Queries.DTOs;

public class CartPaymentDTO
{
    public string CreditCardName { get; set; }
    public string CreditCardNumber { get; set; }
    public string CreditCardExpirationDate { get; set; }
    public string CreditCardCVV { get; set; }

}