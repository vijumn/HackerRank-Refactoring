namespace Refactoring.LegacyService
{
    using System;
    using Refactoring.LegacyService.CreditProvider;
    using Refactoring.LegacyService.DataAccess;
    using Refactoring.LegacyService.Models;
    using Refactoring.LegacyService.Repostories;
    using Refactoring.LegacyService.Services;
    using Refactoring.LegacyService.Validators;

    public class CandidateService
    {
        private readonly ICandidateCreditService _candidateCreditServiceClient;
        private readonly IPositionRepository _positionRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICandidateDataAccess _candidateDataAccess;
        private  readonly CandidateValidator _candidateValidator;

        public CandidateService(ICandidateCreditService CandidateCreditServiceClient, 
            IPositionRepository positionRepository, 
            IDateTimeProvider dateTimeProvider, 
            ICandidateDataAccess candidateDataAccess,
            CandidateValidator candidateValidator
            )
        {
            _candidateCreditServiceClient = CandidateCreditServiceClient;
            _positionRepository = positionRepository;
            _dateTimeProvider = dateTimeProvider;
            _candidateDataAccess = candidateDataAccess;
            _candidateValidator = candidateValidator;
        }
        public CandidateService() : this(
                    new CandidateCreditServiceClient(), new PositionRepository(), new DateTimeProvider(), new CandidateDataAccessProxy() , new CandidateValidator(new DateTimeProvider())
        )
        {
        }
        public bool AddCandidate(string firname, string surname, string email, DateTime dateOfBirth, int positionid)
        {

            if (!_candidateValidator.HasValidName(firname,surname))
            {
                return false;
            }

            if (!_candidateValidator.HasValidEmail(email))
            {
                return false;
            }
          
            if ( !_candidateValidator.IsCandidateAgeAbove18(dateOfBirth))
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


            ICreditProvider provider = CreditProviderFactory.GetCreditProvider(position.Name, _candidateCreditServiceClient);
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
