using System.Data;

namespace RefactoringTest.DataAccess.Interfaces;

public interface IDbCommandWriterWrapper : IDisposable
{
    void AddParameter(string name, SqlDbType dbType, int size, object value);
    Task ExecuteNonQueryAsync();
}