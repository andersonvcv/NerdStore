namespace NerdStore.Payment.AntiCorruption;

public class PaypalGateway : IPaypalGateway
{
    public string GetPaypalServiceKey(string apiKey, string encryptionKey)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUWXYZ123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }

    public string GetCardHashKey(string serviceKey, string cadNumber)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUWXYZ123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }

    public bool CommitTransaction(string cardHashKey, string orderId, decimal amount)
    {
        //return new Random().Next(2) == 0;
        return true;
    }
}