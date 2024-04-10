using NUnit.Framework;
using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Tests
{
    [TestFixture]
    public class SecuritySpecialistTests
    {
        private SecuritySpecialist _securitySpecialist;

        [SetUp]
        public void SetUp()
        {
            var dateOfBirth = new DateTime(1992, 12, 26);
            var firstname = "Daniel";
            var surname = "Brazil";
            var email = "dgamma3@gmail.com";
            var positionId = 1;
            var position = new Position();
            _securitySpecialist = new SecuritySpecialist(firstname, surname, email, dateOfBirth, positionId, position);
        }

        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(3000)]
        public void Should_CreateSecuritySpecialist_With_ValidCreditScore(int creditScore)
        {
            _securitySpecialist.UpdateCreditAndPerformChecksIfRequired(creditScore);
            Assert.That(_securitySpecialist.RequireCreditCheck, Is.EqualTo(true));
            Assert.That(creditScore / 2, Is.EqualTo(_securitySpecialist.Credit));
        }

        [TestCase(999)]
        [TestCase(500)]
        [TestCase(1)]
        public void Should_ThrowArgumentException_When_InvalidCreditScore(int creditScore)
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _securitySpecialist.UpdateCreditAndPerformChecksIfRequired(creditScore));
            Assert.That(ex.Message, Contains.Substring("Candidate does not meet the minimum score"));
        }
    }
}