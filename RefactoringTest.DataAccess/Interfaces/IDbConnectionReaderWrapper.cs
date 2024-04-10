namespace RefactoringTest.DataAccess.Interfaces;

public interface IDbConnectionReaderWrapper : IDisposable
{
    Task OpenAsync();
    IDbCommandReaderWrapper CreateCommand(string commandText); 
    void createConnection(string connectionString);
}