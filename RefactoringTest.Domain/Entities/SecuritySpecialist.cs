using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Entities
{
    public class SecuritySpecialist : Candidate
    {
        public SecuritySpecialist(string firstname, string surname, string email, DateTime dateOfBirth, int positionId,
            Position position)
            : base(firstname, surname, email, dateOfBirth, positionId, position)
        {
        }

        public override void UpdateCreditAndPerformChecksIfRequired(int credit)
        {
            Credit = credit / 2;
            RequireCreditCheck = true;
            CreditScoreCheck();
        }
    }
}