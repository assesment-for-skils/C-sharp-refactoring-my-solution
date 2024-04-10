using System.CodeDom.Compiler;

namespace RefactoringTest.DataAccess
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface ICandidateCreditService
    {
        int GetCredit(string firstname, string surname, DateTime dateOfBirth);
    }
    /// removed other code here to get the project to build
}