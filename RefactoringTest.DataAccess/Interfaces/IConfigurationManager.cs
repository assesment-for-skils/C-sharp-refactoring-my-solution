namespace RefactoringTest.DataAccess.Interfaces
{
    public interface IConfigurationManager
    {
        string GetConnectionString(string name);
    }
}