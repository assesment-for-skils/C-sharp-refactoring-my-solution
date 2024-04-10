using System.Data;
using System.Data.SqlClient;

namespace RefactoringTest.DataAccess.Interfaces;

public class SqlConnectionWriterWrapper : IDbConnectionWriterWrapper
{
    private SqlConnection _connection;

    public void createConnection(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public async Task OpenAsync() => await _connection.OpenAsync();

    public IDbCommandWriterWrapper CreateCommand(string commandText) => new SqlCommandWriterWrapper(new SqlCommand
    {
        Connection = _connection,
        CommandType = CommandType.StoredProcedure,
        CommandText = commandText
    });

    public void Dispose() => _connection.Dispose();
}