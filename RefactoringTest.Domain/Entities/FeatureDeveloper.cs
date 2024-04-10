using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Entities
{
    public class FeatureDeveloper : Candidate
    {
        public FeatureDeveloper(string firstname, string surname, string email, DateTime dateOfBirth, int positionId,
            Position position)
            : base(firstname, surname, email, dateOfBirth, positionId, position)
        {
        }

        public override void UpdateCreditAndPerformChecksIfRequired(int credit)
        {
            Credit = credit;
            RequireCreditCheck = true;
            CreditScoreCheck();
        }
    }
}