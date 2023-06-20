namespace Refactoring.LegacyService.CreditProvider
{
    public interface ICreditProviderFactory
    {
        ICreditProvider GetCreditProvider(string position);
    }
}