using Refactoring.LegacyService.Models;
using Refactoring.LegacyService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.Validators
{
    public  class CandidateValidator
    {
        private readonly  IDateTimeProvider  _dateTimeProvider;

        public CandidateValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public bool HasCreditLessthan500(Candidate candidate)
        {
            if (candidate.RequireCreditCheck && candidate.Credit < 500)
            {
                return true;
            }
            return false;
        }

        public bool HasValidName(string firstName, string surName)
        {

            if (string.IsNullOrEmpty(firstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(surName))
            {
                return false;
            }

            return true;

        }

        public bool HasValidEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        public bool IsCandidateAgeAbove18(DateTime dateofBirth)
        {
            var now = _dateTimeProvider.Now;
            int age = now.Year - dateofBirth.Year;

            if (now.Month < dateofBirth.Month || (now.Month == dateofBirth.Month && now.Day < dateofBirth.Day))
            {
                age--;
            }

            if (age < 18)
            {
                return false;
            }

            return true;
        }
    }
}
