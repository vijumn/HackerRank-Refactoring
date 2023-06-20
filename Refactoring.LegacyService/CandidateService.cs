namespace Refactoring.LegacyService
{
    using Refactoring.LegacyService.CreditProvider;
    using Refactoring.LegacyService.DataAccess;
    using Refactoring.LegacyService.Models;
    using Refactoring.LegacyService.Repostories;
    using Refactoring.LegacyService.Services;
    using Refactoring.LegacyService.Validators;
    using System;

    public class CandidateService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ICandidateDataAccess _candidateDataAccess;
        private readonly ICandidateValidator _candidateValidator;
        private readonly ICreditProviderFactory _creditProviderFactory;

        public CandidateService(
            IPositionRepository positionRepository,
            ICandidateDataAccess candidateDataAccess,
            CandidateValidator candidateValidator
,
            ICreditProviderFactory creditProviderFactory)
        {
            _positionRepository = positionRepository;
            _candidateDataAccess = candidateDataAccess;
            _candidateValidator = candidateValidator;
            _creditProviderFactory = creditProviderFactory;
        }

        public CandidateService() : this(
                     new PositionRepository(),
                     new CandidateDataAccessProxy(),
                     new CandidateValidator(new DateTimeProvider()),
                     new CreditProviderFactory(new CandidateCreditServiceClient())
        )
        {
        }

        public bool AddCandidate(string firname, string surname, string email, DateTime dateOfBirth, int positionid)
        {
            var candidate = new Candidate
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

            if (!_candidateValidator.IsValid(candidate))
            {
                return false;
            }

            var position = _positionRepository.GetById(positionid);
            candidate.Position = position;

            ICreditProvider provider = _creditProviderFactory.GetCreditProvider(position.Name);
            var creditprovided = provider.GetCreditLimit(candidate);

            if (_candidateValidator.HasCreditLessThan500(creditprovided))
            {
                return false;
            }

            _candidateDataAccess.AddCandidate(candidate);

            return true;
        }
    }
}