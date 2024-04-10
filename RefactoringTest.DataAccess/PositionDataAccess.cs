using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RefactoringTest.DataAccess.Interfaces;
using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.DataAccess
{
    public class PositionDataAccess : IPositionDataAccess
    {
        private readonly IDbConnectionReaderWrapper _connectionReaderWrapper;
        private readonly IConfigurationManager _configurationManager;

        public PositionDataAccess(IDbConnectionReaderWrapper reader, IConfigurationManager configurationManager)
        {
            _connectionReaderWrapper = reader;
            _configurationManager = configurationManager;

        }

        public async Task<Position> GetByIdAsync(int id)
        {
            try
            {

                var connectionString = _configurationManager.GetConnectionString("applicationDatabase");
                
                _connectionReaderWrapper.createConnection(connectionString);
                await _connectionReaderWrapper.OpenAsync();
                using (var command = _connectionReaderWrapper.CreateCommand("uspGetPositionById"))
                {
                    command.AddParameter("@positionId", id);

                    using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Position
                            {
                                Id = reader.GetInt32("positionId"),
                                Name = reader.GetString("Name"),
                                Status = (PositionStatus)reader.GetInt32(("Status"))
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"SQL error occurred: {ex.Message}");
            }

            return null;

        }
    }
}


