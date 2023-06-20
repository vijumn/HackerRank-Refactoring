using Refactoring.LegacyService.Models;
using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.Validators
{

    public class CandidateValidator : ICandidateValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CandidateValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public bool IsValid(Candidate candidate)
        {
            return
                HasValidName(candidate.Firstname, candidate.Surname) &&
                HasValidEmail(candidate.EmailAddress) &&
                IsCandidateAgeAbove18(candidate.DateOfBirth);
        }

        public  bool HasCreditLessThan500(CreditLimit creditLimit)
        {
            return creditLimit.HasCreditLimit && creditLimit.Credit < 500;
        }

        private bool HasValidName(string firstName, string surName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(surName);
        }

        private bool HasValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsCandidateAgeAbove18(DateTime dateofBirth)
        {
            var now = _dateTimeProvider.Now;
            int age = now.Year - dateofBirth.Year;

            if (now.Month < dateofBirth.Month || (now.Month == dateofBirth.Month && now.Day < dateofBirth.Day))
            {
                age--;
            }

            return age >= 18;
        }
    }
}
