using RefactoringTest.DataAccess;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Factories;

namespace RefactoringTest.Services
{
    public class CandidateService
    {
        private readonly IDbConnectionWriterWrapper _connectionWriterWrapper;
        private readonly IPositionDataAccess _positionDataAccess;
        private readonly ICandidateCreditService _candidateCreditServices;
        private readonly IConfigurationManager _configurationManager;

        public CandidateService(IDbConnectionWriterWrapper writer, IPositionDataAccess positionDataAccess,
            ICandidateCreditService candidateCreditService, IConfigurationManager configurationManager)
        {
            _connectionWriterWrapper = writer;
            _positionDataAccess = positionDataAccess;
            _candidateCreditServices = candidateCreditService;
            _configurationManager = configurationManager;
        }

        public async Task<bool> AddCandidate(string firstname, string surname, string email, DateTime dateOfBirth,
            int positionId)
        {
            try
            {
                var connectionString = _configurationManager.GetConnectionString("applicationDatabase");

                var position = await _positionDataAccess.GetByIdAsync(positionId);

                var candidate = CandidateFactory.CreateCandidate(position.Name, firstname, surname, email, dateOfBirth,
                    positionId, position);

                var credit =
                    _candidateCreditServices.GetCredit(candidate.Firstname, candidate.Surname, candidate.DateOfBirth);
                candidate.UpdateCreditAndPerformChecksIfRequired(credit);

                _connectionWriterWrapper.createConnection(connectionString);
                await CandidateDataAccess.AddCandidateAsync(candidate, _connectionWriterWrapper);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}