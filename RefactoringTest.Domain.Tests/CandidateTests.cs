using NUnit.Framework;
using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Tests
{
    [TestFixture]
    public class CandidateTests
    {
        private const string Firstname = "Daniel";
        private const string Surname = "Brazil";
        private const string Email = "dgamma3@gmail.com";
        private static readonly DateTime DateOfBirth = new DateTime(1992, 12, 26);
        private const int PositionId = 1;
        private readonly Position position = new Position();

        [Test]
        public void Constructor_WithValidParameters_ShouldInstantiateCorrectly()
        {
            var candidate = new Candidate(Firstname, Surname, Email, DateOfBirth, PositionId, position);
            Assert.Multiple(() =>
            {
                Assert.That(candidate.Firstname, Is.EqualTo(Firstname));
                Assert.That(candidate.Surname, Is.EqualTo(Surname));
                Assert.That(candidate.EmailAddress, Is.EqualTo(Email));
                Assert.That(candidate.DateOfBirth, Is.EqualTo(DateOfBirth));
                Assert.That(candidate.PositionId, Is.EqualTo(PositionId));
                Assert.That(candidate.Position, Is.EqualTo(position));
            });
        }

        [TestCase(null)]
        [TestCase("")]
        public void Constructor_WithInvalidFirstname_ShouldThrowArgumentException(string invalidFirstname)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Candidate(invalidFirstname, Surname, Email, DateOfBirth, PositionId, position));
            StringAssert.Contains("Firstname cannot be empty.", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Constructor_WithInvalidSurname_ShouldThrowArgumentException(string invalidSurname)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Candidate(Firstname, invalidSurname, Email, DateOfBirth, PositionId, position));
            StringAssert.Contains("Surname cannot be empty.", ex.Message);
        }

        [TestCase("notanemail")]
        [TestCase("missing@dotcom")]
        public void Constructor_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Candidate(Firstname, Surname, invalidEmail, DateOfBirth, PositionId, position));
            StringAssert.Contains("Invalid email address.", ex.Message);
        }

        [TestCase(17)]
        [TestCase(1)]
        [TestCase(-10)]
        public void Constructor_WithUnderageCandidate_ShouldThrowArgumentException(int invalidAge)
        {
            var underageDateOfBirth = DateTime.Now.AddYears(-invalidAge);
            var ex = Assert.Throws<ArgumentException>(() =>
                new Candidate(Firstname, Surname, Email, underageDateOfBirth, PositionId, position));
            StringAssert.Contains("Candidate must be at least 18 years old.", ex.Message);
        }

        [TestCase(500)]
        [TestCase(600)]
        [TestCase(700)]
        [TestCase(499)]
        [TestCase(1)]
        [TestCase(-1)]
        public void Should_CreateCandidate_Regardless_Of_CreditScore(int creditScore)
        {
            var candidate = new Candidate(Firstname, Surname, Email, DateOfBirth, PositionId, position);
            candidate.UpdateCreditAndPerformChecksIfRequired(creditScore);
            Assert.Multiple(() =>
            {
                Assert.IsFalse(candidate.RequireCreditCheck);
                Assert.That(candidate.Credit, Is.EqualTo(creditScore));
            });
        }
    }
}