using System;
using Source.Common.Components.Implementations.SelectablesGroup;
using UnityEngine;

namespace Source.Controllers.Api.Mediators
{
    public interface IUIMediator
    {
        event Action<int> LevelSelected;
        event Action<int> LevelStageSelected;
        event Action<int> LevelStarted;
        event Action<int> LevelClosed;
        bool IsPreviousStageAvailable { get; }
        bool IsNextStageAvailable { get; }
        void BuildMainMenuView();
        void BindMainMenuComponents(Transform levelContainer, Transform stageContainer);
        int GetSelectedLevelId();
        int GetSelectedLevelStageId();
        void SelectLevel(int id);
        void SelectLevelStage(int id);
        int GetMaxStageId();
        void SelectNextStage();
        void SelectPreviousStage();
        void StartLevel();
        void CloseLevel();
        void StartGameLoop();
        void BindSelectableLevelsGroup(SelectablesUiGroup selectableLevels);
        void BindSelectableLevelStagesGroup(SelectablesUiGroup selectableLevelStages);
        void RebuildLevelStageViews();
    }
}