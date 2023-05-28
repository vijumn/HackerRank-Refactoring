using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.CreditProvider
{
    public  class CreditProviderFactory
    {
        public static ICreditProvider GetCreditProvider(string position, ICandidateCreditService candidateCreditServiceClient)
        {
            switch (position)
            {
                case "SecuritySpecialist":
                    return new SecuritySpecialistCreaditProvider(candidateCreditServiceClient);
                case "FeatureDeveloper":
                    return new FeatureDeveloperCreditProvider(candidateCreditServiceClient);
                default:
                    return new DefaultCreditProvider(candidateCreditServiceClient);
            }
        }
    }
}
