using System.Data;
using System.Data.SqlClient;

namespace RefactoringTest.DataAccess.Interfaces
{
    public class SqlCommandWrapper : IDbCommandReaderWrapper
    {
        private SqlCommand _command;

        public SqlCommandWrapper(SqlCommand command)
        {
            _command = command;
        }

        public void AddParameter(string name, object value)
        {
            _command.Parameters.AddWithValue(name, value);
        }

        public async Task<IExecuteReaderAsync> ExecuteReaderAsync(CommandBehavior connectionBehaviour)
        {
            var reader = await _command.ExecuteReaderAsync(connectionBehaviour);
            return new ExecuteReaderAsyncWrapper(reader);
        }

        public void Dispose() => _command.Dispose();
    }


}
