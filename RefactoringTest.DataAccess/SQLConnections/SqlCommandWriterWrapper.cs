using System.Data;
using System.Data.SqlClient;

namespace RefactoringTest.DataAccess.Interfaces;

public class SqlCommandWriterWrapper : IDbCommandWriterWrapper
{
    private readonly SqlCommand _command;

    public SqlCommandWriterWrapper(SqlCommand command)
    {
        _command = command;
    }

    public void AddParameter(string name, SqlDbType dbType, int size, object value)
    {
        _command.Parameters.Add(new SqlParameter(name, dbType, size) { Value = value });
    }

    public async Task ExecuteNonQueryAsync() => await _command.ExecuteNonQueryAsync();


    public void Dispose() => _command.Dispose();
}