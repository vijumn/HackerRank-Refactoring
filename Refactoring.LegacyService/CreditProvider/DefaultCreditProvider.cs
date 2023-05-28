using Refactoring.LegacyService.Models;
using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.CreditProvider
{
    public class DefaultCreditProvider : ICreditProvider
    {
        private readonly ICandidateCreditService _candidateCreditServiceClient;

        public DefaultCreditProvider(ICandidateCreditService candidateCreditServiceClient)
        {
            _candidateCreditServiceClient = candidateCreditServiceClient;
        }

        public CreditLimit GetCreditLimit(Candidate candidate)
        {
            return new CreditLimit
            {
                HasCreditLimit = false
            };
        }

        public string PositionProvider => string.Empty;
    }

}
