namespace NerdStore.Payment.AntiCorruption;

public interface IPaypalGateway
{
    string GetPaypalServiceKey(string apiKey, string encryptionKey);
    string GetCardHashKey(string serviceKey, string cadNumber);
    bool CommitTransaction(string cardHashKey, string orderId, decimal amount);
}