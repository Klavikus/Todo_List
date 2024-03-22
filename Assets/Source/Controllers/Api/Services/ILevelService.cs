using System;

namespace Source.Controllers.Api.Services
{
    public interface ILevelService
    {
        event Action<int> LevelSelected;
        event Action<int> LevelStageSelected;
        event Action<int> LevelStarted;
        event Action<int> LevelClosed;
        int SelectedLevelId { get; }
        int SelectedLevelStageId { get; }
        int SelectedLevelMaxStageId { get; }
        bool IsNextStageAvailable { get; }
        bool IsPreviousStageAvailable { get; }
        void SelectLevel(int levelId);
        void SelectLevelStage(int levelId);
        void SelectNextStage();
        void SelectPreviousStage();
        void StartLevel();
        void CloseLevel();
    }
}