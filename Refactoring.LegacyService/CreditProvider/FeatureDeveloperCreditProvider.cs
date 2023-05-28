using Refactoring.LegacyService.Models;
using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.CreditProvider
{
    public class FeatureDeveloperCreditProvider: ICreditProvider
    {
        private readonly ICandidateCreditService _candidateCreditServiceClient;

        public FeatureDeveloperCreditProvider(ICandidateCreditService candidateCreditServiceClient)
        {
            _candidateCreditServiceClient = candidateCreditServiceClient;
        }

        public CreditLimit GetCreditLimit(Candidate candidate)
        {
            var credit = _candidateCreditServiceClient.GetCredit(candidate.Firstname, candidate.Surname, candidate.DateOfBirth);
            return new CreditLimit
            {
                Credit = credit,
                HasCreditLimit = true
            };
        }

        public string PositionProvider => "FeatureDeveloper";
    }
}
