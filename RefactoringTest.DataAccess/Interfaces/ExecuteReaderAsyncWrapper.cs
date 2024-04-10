using System.Data;
using System.Data.SqlClient;

namespace RefactoringTest.DataAccess.Interfaces;

class ExecuteReaderAsyncWrapper : IExecuteReaderAsync
{
    private SqlDataReader _reader;

    public ExecuteReaderAsyncWrapper(SqlDataReader reader)
    {
        _reader = reader;
    }

    public async Task<bool> ReadAsync() => await _reader.ReadAsync();

    public int GetInt32(string name) => _reader.GetInt32(name);

    public string GetString(string name) => _reader.GetString(name);

    public int GetOrdinal(string positionid)
    {
        return _reader.GetOrdinal(positionid);
    }

    public void Dispose() => _reader.Dispose();
}