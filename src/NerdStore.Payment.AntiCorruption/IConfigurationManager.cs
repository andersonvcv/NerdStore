namespace NerdStore.Payment.AntiCorruption;

public interface IConfigurationManager
{
    string GetValue(string node);
}