using RefactoringTest.Domain.Entities;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Factories
{
    public static class CandidateFactory
    {
        public static Candidate CreateCandidate(string positionName, string firstname, string surname, string email, DateTime dateOfBirth, int positionId, Position position)
        {
            switch (positionName)
            {
                case "SecuritySpecialist":
                    return new SecuritySpecialist(firstname, surname, email, dateOfBirth, positionId, position);
                case "FeatureDeveloper":
                    return new FeatureDeveloper(firstname, surname, email, dateOfBirth, positionId, position);
                default:
                    return new Candidate(firstname, surname, email, dateOfBirth, positionId, position);

            }
        }
    }
}