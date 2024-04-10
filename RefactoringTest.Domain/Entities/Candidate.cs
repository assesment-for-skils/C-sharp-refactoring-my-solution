using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.Domain.Entities
{
    public class Candidate
    {
        private string firstname;

        public string Firstname
        {
            get => firstname;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Firstname cannot be empty.");
                }

                firstname = value;
            }
        }

        private string surname;

        public string Surname
        {
            get => surname;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Surname cannot be empty.");
                }

                surname = value;
            }
        }

        private string emailAddress;

        public string EmailAddress
        {
            get => emailAddress;
            private set
            {
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ArgumentException("Invalid email address.");
                }

                emailAddress = value;
            }
        }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            private set
            {
                if (CalculateAge(value) < 18)
                {
                    throw new ArgumentException("Candidate must be at least 18 years old.");
                }

                dateOfBirth = value;
            }
        }

        public int PositionId { get; private set; }
        public Position Position { get; set; }
        public bool RequireCreditCheck { get; protected set; }
        public int Credit { get; protected set; }

        public Candidate(string firstname, string surname, string email, DateTime dateOfBirth, int positionId,
            Position position)
        {
            this.Firstname = firstname;
            this.Surname = surname;
            this.EmailAddress = email;
            this.DateOfBirth = dateOfBirth;
            this.PositionId = positionId;
            this.Position = position;
        }

        public void CreditScoreCheck()
        {
            if (RequireCreditCheck && Credit < 500)
            {
                throw new InvalidOperationException("Candidate does not meet the minimum score");
            }
        }

        protected static int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }

        public virtual void UpdateCreditAndPerformChecksIfRequired(int credit)
        {
            Credit = credit;
            RequireCreditCheck = false;
            CreditScoreCheck();
        }
    }
}