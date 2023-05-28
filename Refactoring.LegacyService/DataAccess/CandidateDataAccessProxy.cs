using Refactoring.LegacyService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.DataAccess
{
    public  class CandidateDataAccessProxy : ICandidateDataAccess
    {
        public void AddCandidate(Candidate candidate)
        {
            CandidateDataAccess.AddCandidate(candidate);
        }
    }
}
