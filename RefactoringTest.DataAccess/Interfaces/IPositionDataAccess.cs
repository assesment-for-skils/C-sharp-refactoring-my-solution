using RefactoringTest.Domain.Entities.ValueObjects;

namespace RefactoringTest.DataAccess.Interfaces
{
    public interface IPositionDataAccess
    {
        Task<Position> GetByIdAsync(int id);
    }
}