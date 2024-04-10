using NUnit.Framework;
using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;
using RefactoringTest.Domain.Factories;

namespace RefactoringTest.Domain.Tests
{
    [TestFixture]
    public class CandidateFactoryTests
    {
        private const string Firstname = "Daniel";
        private const string Surname = "Brazil";
        private const string Email = "dgamma3@gmail.com";
        private readonly DateTime _dateOfBirth = new DateTime(1992, 12, 26);
        private const int PositionId = 1;
        private readonly Position _position = new Position { Id = 1, Name = "Software Developer" };

        [Test]
        public void CreateCandidate_WhenSecuritySpecialist_ShouldReturnSecuritySpecialistType()
        {
            var candidate = CandidateFactory.CreateCandidate("SecuritySpecialist", Firstname, Surname, Email,
                _dateOfBirth, PositionId, _position);
            Assert.IsInstanceOf<SecuritySpecialist>(candidate);
            AssertCandidateProperties(candidate);
        }

        [Test]
        public void CreateCandidate_WhenFeatureDeveloper_ShouldReturnFeatureDeveloperType()
        {
            var candidate = CandidateFactory.CreateCandidate("FeatureDeveloper", Firstname, Surname, Email,
                _dateOfBirth, PositionId, _position);
            Assert.IsInstanceOf<FeatureDeveloper>(candidate);
            AssertCandidateProperties(candidate);
        }

        [Test]
        public void CreateCandidate_WhenUnknownPosition_ShouldReturnDefaultCandidateType()
        {
            var candidate = CandidateFactory.CreateCandidate("UnknownPosition", Firstname, Surname, Email, _dateOfBirth,
                PositionId, _position);
            Assert.IsInstanceOf<Candidate>(candidate);
            AssertCandidateProperties(candidate);
        }

        private void AssertCandidateProperties(Candidate candidate)
        {
            Assert.Multiple(() =>
            {
                Assert.That(candidate.Firstname, Is.EqualTo(Firstname));
                Assert.That(candidate.Surname, Is.EqualTo(Surname));
                Assert.That(candidate.EmailAddress, Is.EqualTo(Email));
                Assert.That(candidate.DateOfBirth, Is.EqualTo(_dateOfBirth));
                Assert.That(candidate.PositionId, Is.EqualTo(PositionId));
                Assert.That(candidate.Position, Is.EqualTo(_position));
            });
        }
    }
}