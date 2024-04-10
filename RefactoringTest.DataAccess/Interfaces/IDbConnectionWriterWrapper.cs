namespace RefactoringTest.DataAccess.Interfaces;

public interface IDbConnectionWriterWrapper : IDisposable
{
    Task OpenAsync();
    IDbCommandWriterWrapper CreateCommand(string commandText);
    void createConnection(string connectionString);
}