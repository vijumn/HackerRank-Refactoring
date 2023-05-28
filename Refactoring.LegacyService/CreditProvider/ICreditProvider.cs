using Refactoring.LegacyService.Models;

namespace Refactoring.LegacyService.CreditProvider
{
    public interface ICreditProvider
    {
        string PositionProvider { get; }

        CreditLimit GetCreditLimit(Candidate candidate);
    }
}