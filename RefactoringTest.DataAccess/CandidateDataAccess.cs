using System.Data;
using System.Data.SqlClient;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Entities;

namespace RefactoringTest.DataAccess
{
    public static class CandidateDataAccess
    {
        public static async Task AddCandidateAsync(Candidate candidate,
            IDbConnectionWriterWrapper connectionWriterWrapper)
        {
            try
            {
                await connectionWriterWrapper.OpenAsync();
                using (var command = connectionWriterWrapper.CreateCommand("uspAddCandidate"))
                {
                    command.AddParameter("@Firstname", SqlDbType.VarChar, 50, candidate.Firstname);
                    command.AddParameter("@Surname", SqlDbType.VarChar, 50, candidate.Surname);
                    command.AddParameter("@DateOfBirth", SqlDbType.DateTime, 0, candidate.DateOfBirth);
                    command.AddParameter("@EmailAddress", SqlDbType.VarChar, 50, candidate.EmailAddress);
                    command.AddParameter("@RequireCreditCheck", SqlDbType.Bit, 0, candidate.RequireCreditCheck);
                    command.AddParameter("@Credit", SqlDbType.Int, 0, candidate.Credit);
                    command.AddParameter("@PositionId", SqlDbType.Int, 0, candidate.Position.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"SQL error occurred: {ex.Message}");
            }
        }
    }
}