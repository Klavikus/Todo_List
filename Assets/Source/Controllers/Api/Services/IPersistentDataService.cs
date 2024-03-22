using Source.Domain;
using Source.Domain.Data;

namespace Source.Controllers.Api.Services
{
    public interface IPersistentDataService
    {
        void ClearData();
        void UpdateLevelProgress(Level level);
        LevelProgress GetLevelProgress(Level level);
    }
}