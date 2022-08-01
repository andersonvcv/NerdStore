namespace NerdStore.Payment.AntiCorruption;

public class ConfigurationManager : IConfigurationManager
{
    public string GetValue(string node)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUWXYZ123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
}