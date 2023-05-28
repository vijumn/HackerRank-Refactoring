using Refactoring.LegacyService.Models;
using System;
using Xunit;
using Moq;
using Refactoring.LegacyService.DataAccess;
using Refactoring.LegacyService.Repostories;
using Refactoring.LegacyService.Services;

namespace Refactoring.LegacyService.Tests
{
    public class CandidateServiceTests
    {
        private readonly CandidateService _sut;
        private readonly Mock<ICandidateDataAccess> _candidateDataAccess =  new Mock<ICandidateDataAccess>();
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new Mock<IDateTimeProvider>();
        private readonly Mock<IPositionRepository> _positionRespository = new Mock<IPositionRepository>();
        private readonly Mock<ICandidateCreditService> _candidateCreditService =new Mock<ICandidateCreditService>();

        public CandidateServiceTests()
        {
            _sut = new CandidateService(_candidateCreditService.Object,_positionRespository.Object, _dateTimeProvider.Object, _candidateDataAccess.Object, new Validators.CandidateValidator(_dateTimeProvider.Object));
        }

        [Fact]
        public void Add_Candidate_Should_AddCandicate_Specialist_Age_20()
        {
            //Arrange
            _dateTimeProvider.Setup(x => x.Now).Returns( new DateTime(2023,05,28 ));
            _positionRespository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Position { Name = "SecuritySpecialist" });
            _candidateCreditService.Setup(x => x.GetCredit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1000);
            _candidateDataAccess.Setup(x => x.AddCandidate(It.IsAny<Candidate>()));
           
            //Act
            var result =  _sut.AddCandidate("First Name","Sur Name","email@email.cc", DateTime.Now.AddYears(-20),1);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void Add_Candidate_Should_AddCandicate_FeatureDeveloper_Age_20()
        {
            //Arrange
            _dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2023, 05, 28));
            _positionRespository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Position { Name = "FeatureDeveloper" });
            _candidateCreditService.Setup(x => x.GetCredit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            _candidateDataAccess.Setup(x => x.AddCandidate(It.IsAny<Candidate>()));

            //Act
            var result = _sut.AddCandidate("First Name", "Sur Name", "email@email.cc", DateTime.Now.AddYears(-20), 1);

            //Assert
            Assert.True(result);

        }

        [Theory]
        [InlineData("First Name", "Sur Name", "email", 17, "SecuritySpecialist")]
        [InlineData("First Name", "Sur Name", "email@.cc", 17, "SecuritySpecialist")]
        [InlineData("First Name", "Sur Name", "email.cc", 17, "SecuritySpecialist")]
        [InlineData("First Name", "Sur Name", "email.cc", 18, "SecuritySpecialist")]
        public void AddCandidate_DataValidation_Fails(string firstName, string surName, string email, int age, string position)
        {
            //Arrange
            _dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2023, 05, 28));
            _positionRespository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Position { Name = position });
            _candidateCreditService.Setup(x => x.GetCredit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1000);
            _candidateDataAccess.Setup(x => x.AddCandidate(It.IsAny<Candidate>()));

            //Act
            var result = _sut.AddCandidate(firstName,surName ,email , DateTime.Now.AddYears(-1*age), 1);

            //Assert
            Assert.False(result);

        }


    }
}
