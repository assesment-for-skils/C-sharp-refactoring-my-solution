using System.Data;

namespace RefactoringTest.DataAccess.Interfaces;

public interface IDbCommandReaderWrapper : IDisposable
{
    void AddParameter(string name, object value);
    Task<IExecuteReaderAsync> ExecuteReaderAsync(CommandBehavior connectionBehaviour);
}