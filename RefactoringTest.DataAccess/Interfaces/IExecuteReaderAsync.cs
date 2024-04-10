namespace RefactoringTest.DataAccess.Interfaces;

public interface IExecuteReaderAsync : IDisposable
{
    Task<bool> ReadAsync();
    int GetInt32(string name);
    string GetString(string name);
}