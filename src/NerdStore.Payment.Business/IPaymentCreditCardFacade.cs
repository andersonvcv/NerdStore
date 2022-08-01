namespace NerdStore.Payment.Business;

public interface IPaymentCreditCardFacade
{
    Transaction Pay(Request request, Payment payment);
}