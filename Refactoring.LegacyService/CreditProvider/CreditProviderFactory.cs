using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Refactoring.LegacyService.CreditProvider
{
    public  class CreditProviderFactory
    {
        private readonly IReadOnlyDictionary<string, ICreditProvider> _creditProviders;
        private readonly ICandidateCreditService _candidateCreditServiceClient;

        public CreditProviderFactory(ICandidateCreditService candidateCreditServiceClient)
        {
            _candidateCreditServiceClient = candidateCreditServiceClient;

            var providerType= typeof(ICreditProvider);
            _creditProviders = providerType.Assembly.ExportedTypes
                .Where(p => providerType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(c =>
                {
                    var paramlessCtor = c.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                    if (paramlessCtor == null)
                    {
                        return Activator.CreateInstance(c, _candidateCreditServiceClient) as ICreditProvider;
                    }
                    return Activator.CreateInstance(c) as ICreditProvider;
                }).ToDictionary(c=>c.PositionProvider,c=>c);
        }

      

        public  ICreditProvider GetCreditProvider(string position) 
        {
            return _creditProviders.TryGetValue(position, out var provider) ? provider : new DefaultCreditProvider();
        }
    }
}
