using System.Data;
using Moq;
using NUnit.Framework;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.DataAccess.Test
{
    [TestFixture]
    public class PositionDataAccessTests
    {
        [Test]
        public async Task GetByIdAsync_ReturnsPosition_WhenIdExists()
        {
            var mockReader = new Mock<IDbConnectionReaderWrapper>();
            var mockConfiguration = new Mock<IConfigurationManager>();
            var mockCommand = new Mock<IDbCommandReaderWrapper>();
            var mockDataReader = new Mock<IExecuteReaderAsync>();

            mockConfiguration.Setup(m => m.GetConnectionString("applicationDatabase"))
                .Returns(It.IsAny<string>());

            mockReader.Setup(m => m.createConnection(It.IsAny<string>()));
            mockReader.Setup(m => m.OpenAsync()).Returns(Task.CompletedTask);
            mockReader.Setup(m => m.CreateCommand(It.IsAny<string>())).Returns(mockCommand.Object);

            mockCommand.Setup(m => m.AddParameter(It.IsAny<string>(), It.IsAny<int>()));
            mockCommand.Setup(m => m.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                .ReturnsAsync(mockDataReader.Object);

            mockDataReader.SetupSequence(m => m.ReadAsync())
                .ReturnsAsync(true);

            mockDataReader.Setup(m => m.GetInt32("positionId")).Returns(1);
            mockDataReader.Setup(m => m.GetString("Name")).Returns("Manager");
            mockDataReader.Setup(m => m.GetInt32("Status")).Returns((int)PositionStatus.none);

            var dataAccess = new PositionDataAccess(mockReader.Object, mockConfiguration.Object);
            
            var result = await dataAccess.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Manager"));
            Assert.That(result.Status, Is.EqualTo(PositionStatus.none));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenRecordDoesNotExist()
        {
            var mockReader = new Mock<IDbConnectionReaderWrapper>();
            var mockConfiguration = new Mock<IConfigurationManager>();
            var mockCommand = new Mock<IDbCommandReaderWrapper>();
            var mockDataReader = new Mock<IExecuteReaderAsync>();

            mockConfiguration.Setup(m => m.GetConnectionString("applicationDatabase"))
                .Returns(It.IsAny<string>());

            mockReader.Setup(m => m.createConnection(It.IsAny<string>()));
            mockReader.Setup(m => m.OpenAsync()).Returns(Task.CompletedTask);
            mockReader.Setup(m => m.CreateCommand(It.IsAny<string>())).Returns(mockCommand.Object);

            mockCommand.Setup(m => m.AddParameter(It.IsAny<string>(), It.IsAny<int>()));
            mockCommand.Setup(m => m.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                .ReturnsAsync(mockDataReader.Object);

            mockDataReader.SetupSequence(m => m.ReadAsync())
                .ReturnsAsync(false); // mocks that reading didn't succeed
            
            var dataAccess = new PositionDataAccess(mockReader.Object, mockConfiguration.Object);
        
            var result = await dataAccess.GetByIdAsync(2);
            
            Assert.IsNull(result);
        }

        [Test]
        public void GetByIdAsync_Throws_Exception_WhenSQLExceptionOccurs()
        {
            var mockReader = new Mock<IDbConnectionReaderWrapper>();
            var mockConfiguration = new Mock<IConfigurationManager>();

            mockConfiguration.Setup(m => m.GetConnectionString(It.IsAny<string>())).Throws(new Exception());

            var dataAccess = new PositionDataAccess(mockReader.Object, mockConfiguration.Object);

            Assert.ThrowsAsync<Exception>(async () => await dataAccess.GetByIdAsync(1));
        }
    }
}