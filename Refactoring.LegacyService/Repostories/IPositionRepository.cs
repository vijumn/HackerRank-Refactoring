using Refactoring.LegacyService.Models;

namespace Refactoring.LegacyService.Repostories
{
    public interface IPositionRepository
    {
        Position GetById(int id);
    }
}