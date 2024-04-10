using Moq;
using NUnit.Framework;
using RefactoringTest.DataAccess;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Entities.ValueObjects;
using System;
using System.Threading.Tasks;

namespace RefactoringTest.Service.Tests
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private Mock<IPositionDataAccess> _mockPositionDataAccess;
        private Mock<ICandidateCreditService> _mockCandidateCreditService;
        private CandidateService _candidateService;

        private const string Firstname = "Daniel";
        private const string Surname = "Brazil";
        private const string Email = "dgamma3@gmail.com";
        private readonly DateTime DateOfBirth = new DateTime(1992, 12, 26);
        private const int PositionId = 1;

        [SetUp]
        public void Setup()
        {
           var mockConnectionWrapper = new Mock<IDbConnectionWriterWrapper>();
            _mockPositionDataAccess = new Mock<IPositionDataAccess>();
            _mockCandidateCreditService = new Mock<ICandidateCreditService>();
            var mockConfigurationManager = new Mock<IConfigurationManager>();

            var mockCommandWrapper = new Mock<IDbCommandWriterWrapper>();
            mockConnectionWrapper.Setup(m => m.CreateCommand(It.IsAny<string>())).Returns(mockCommandWrapper.Object);
             mockConfigurationManager.Setup(x => x.GetConnectionString("applicationDatabase")).Returns("");

            _candidateService = new CandidateService(mockConnectionWrapper.Object, _mockPositionDataAccess.Object, _mockCandidateCreditService.Object, mockConfigurationManager.Object);
        }

        [Test]
        public async Task AddCandidate_ShouldReturnTrue_WhenCandidateIsSuccessfullyAdded()
        {
            var position = new Position { Id = PositionId, Name = "Software Developer", };
            _mockPositionDataAccess.Setup(x => x.GetByIdAsync(PositionId)).ReturnsAsync(position);
            _mockCandidateCreditService.Setup(x => x.GetCredit(Firstname, Surname, DateOfBirth)).Returns(700);
          
            var result = await _candidateService.AddCandidate(Firstname, Surname, Email, DateOfBirth, PositionId);

            Assert.IsTrue(result);
        }
        
        [Test]
        public async Task AddCandidate_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            var expectedException = new Exception("Test exception");
            _mockPositionDataAccess.Setup(x => x.GetByIdAsync(PositionId)).ThrowsAsync(expectedException);
   
            var result = await _candidateService.AddCandidate(Firstname, Surname, Email, DateOfBirth, PositionId);

            Assert.IsFalse(result);
            _mockPositionDataAccess.Verify(x => x.GetByIdAsync(PositionId), Times.Once);
        }
    }
}
