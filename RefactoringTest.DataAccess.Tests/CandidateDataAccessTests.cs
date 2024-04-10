using System.Data;
using Moq;
using NUnit.Framework;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.DataAccess.Test
{
    [TestFixture]
    public class CandidateDataAccessTests
    {
        private Candidate candidate;
        [SetUp]
        public void SetUp()
        {
            candidate = new Candidate(
                "Daniel",
                "Brazil",
                "dgamma3@gmail.com",
                new DateTime(1992, 12, 26),
                1,
                new Position()
                {
                    Id = 1,
                    Name = "Software Developer",
                    Status = PositionStatus.none
                }
            );
        }

        [Test]
        public async Task AddCandidateAsync_CallsExecuteNonQueryAsync_WithCorrectParameters()
        {
    var mockConnectionWrapper = new Mock<IDbConnectionWriterWrapper>();
            var mockCommandWrapper = new Mock<IDbCommandWriterWrapper>();

            mockConnectionWrapper.Setup(m => m.CreateCommand(It.IsAny<string>())).Returns(mockCommandWrapper.Object);

     
            // Act
            await CandidateDataAccess.AddCandidateAsync(candidate, mockConnectionWrapper.Object);

            // Assert
            mockConnectionWrapper.Verify(m => m.OpenAsync(), Times.Once);
            mockCommandWrapper.Verify(m => m.AddParameter("@Firstname", SqlDbType.VarChar, 50, candidate.Firstname),
                Times.Once);
            mockCommandWrapper.Verify(m => m.AddParameter("@Surname", SqlDbType.VarChar, 50, candidate.Surname),
                Times.Once);
            mockCommandWrapper.Verify(m => m.AddParameter("@DateOfBirth", SqlDbType.DateTime, 0, candidate.DateOfBirth),
                Times.Once);
            mockCommandWrapper.Verify(
                m => m.AddParameter("@EmailAddress", SqlDbType.VarChar, 50, candidate.EmailAddress), Times.Once);
            mockCommandWrapper.Verify(
                m => m.AddParameter("@RequireCreditCheck", SqlDbType.Bit, 0, candidate.RequireCreditCheck), Times.Once);
            mockCommandWrapper.Verify(m => m.AddParameter("@Credit", SqlDbType.Int, 0, candidate.Credit), Times.Once);
            mockCommandWrapper.Verify(m => m.AddParameter("@PositionId", SqlDbType.Int, 0, candidate.Position.Id),
                Times.Once);
            mockCommandWrapper.Verify(m => m.ExecuteNonQueryAsync(), Times.Once);
        }

        [Test]
        public async Task GetByIdAsyncWhenSQLExceptionOccurs()
        {
      
            var mockConnectionWrapper = new Mock<IDbConnectionWriterWrapper>();
            mockConnectionWrapper.Setup(m => m.OpenAsync()).ThrowsAsync(new Exception());


            // Act & Assert
            Assert.ThrowsAsync<System.Exception>(async () =>
                await CandidateDataAccess.AddCandidateAsync(candidate, mockConnectionWrapper.Object));
        }
    }
}