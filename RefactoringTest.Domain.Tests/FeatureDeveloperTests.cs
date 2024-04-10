using NUnit.Framework;
using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Tests
{
    [TestFixture]
    public class FeatureDeveloperTests
    {
        private FeatureDeveloper _featureDeveloper;
        
        [SetUp]
        public void SetUp()
        {
            var dateOfBirth = new DateTime(1992, 12, 26);
            var firstname = "Daniel";
            var surname = "Brazil";
            var email = "dgamma3@gmail.com";
            var positionId = 1;
            var position = new Position();
            _featureDeveloper = new FeatureDeveloper(firstname, surname, email, dateOfBirth, positionId, position);
        }

        [TestCase(500)]
        [TestCase(600)]
        [TestCase(700)]
        public void Should_CreateFeatureDeveloper_With_ValidCreditScore(int creditScore)
        {
            _featureDeveloper.UpdateCreditAndPerformChecksIfRequired(creditScore);
            Assert.That(_featureDeveloper.RequireCreditCheck, Is.EqualTo(true));
            Assert.That(creditScore, Is.EqualTo(_featureDeveloper.Credit));
        }

        [TestCase(499)]
        [TestCase(1)]
        [TestCase(-1)]
        public void Should_ThrowArgumentException_When_InvalidCreditScore(int creditScore)
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _featureDeveloper.UpdateCreditAndPerformChecksIfRequired(creditScore));
            Assert.That(ex.Message, Contains.Substring("Candidate does not meet the minimum score"));
        }
    }
}