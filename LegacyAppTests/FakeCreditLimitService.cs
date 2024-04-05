using LegacyApp;

namespace LegacyAppTests;

public class FakeCreditLimitService : ICreditLimitService
{
    private static readonly int[] CreditLimits = [200, 20000, 10000, 3000, 1000];
    private static readonly Random Random = new();

    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        return CreditLimits[Random.Next(CreditLimits.Length)];
    }
}