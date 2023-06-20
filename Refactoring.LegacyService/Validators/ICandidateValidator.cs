using Refactoring.LegacyService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.Validators
{
    public interface ICandidateValidator
    {
        bool IsValid(Candidate candidate);
        bool HasCreditLessThan500(CreditLimit creditLimit);
    }
}
