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
        private readonly CandidateValidator _candidateValidator;
        private readonly CreditProviderFactory _creditProviderFactory;

        public CandidateService(
            IPositionRepository positionRepository,
            ICandidateDataAccess candidateDataAccess,
            CandidateValidator candidateValidator
,
            CreditProviderFactory creditProviderFactory)
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
            if (!_candidateValidator.HasValidName(firname, surname))
            {
                return false;
            }

            if (!_candidateValidator.HasValidEmail(email))
            {
                return false;
            }

            if (!_candidateValidator.IsCandidateAgeAbove18(dateOfBirth))
            {
                return false;
            }

            var position = _positionRepository.GetById(positionid);

            var candidate = new Candidate
            {
                Position = position,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

            ICreditProvider provider = _creditProviderFactory.GetCreditProvider(position.Name);
            var creditprovided = provider.GetCreditLimit(candidate);

            if (_candidateValidator.HasCreditLessthan500(creditprovided))
            {
                return false;
            }

            _candidateDataAccess.AddCandidate(candidate);

            return true;
        }
    }
}