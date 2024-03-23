using System;
using System.Collections.Generic;
using Source.Common.Components.Implementations.SelectablesGroup;
using Source.Controllers.Api.Mediators;
using Source.Controllers.Api.Services;
using Source.Infrastructure.Api.Builders;
using Source.Presentation.Api;
using UnityEngine;

namespace Source.Controllers.Core.Mediators
{
    public class UIMediator : IUIMediator
    {
        private readonly IViewBuilder _viewBuilder;
        private readonly ILevelService _levelService;

        private Transform _levelContainer;
        private Transform _stageContainer;
        private SelectablesUiGroup _selectableLevelsGroup;
        private SelectablesUiGroup _selectableLevelStagesGroup;

        public UIMediator(
            IViewBuilder viewBuilder,
            ILevelService levelService
        )
        {
            _viewBuilder = viewBuilder;
            _levelService = levelService;

            _levelService.LevelSelected += i => LevelSelected?.Invoke(i);
            _levelService.LevelStageSelected += i => LevelStageSelected?.Invoke(i);
            _levelService.LevelStarted += i => LevelStarted?.Invoke(i);
            _levelService.LevelClosed += i => LevelClosed?.Invoke(i);
        }

        public event Action<int> LevelSelected;
        public event Action<int> LevelStageSelected;
        public event Action<int> LevelStarted;
        public event Action<int> LevelClosed;
        public bool IsPreviousStageAvailable => _levelService.IsPreviousStageAvailable;
        public bool IsNextStageAvailable => _levelService.IsNextStageAvailable;

        public void BindMainMenuComponents(Transform levelContainer, Transform stageContainer)
        {
            _levelContainer = levelContainer;
            _stageContainer = stageContainer;
        }

        public void BindSelectableLevelsGroup(SelectablesUiGroup selectableLevels) =>
            _selectableLevelsGroup = selectableLevels;

        public void BindSelectableLevelStagesGroup(SelectablesUiGroup selectableLevelStages) =>
            _selectableLevelStagesGroup = selectableLevelStages;

        public void BuildMainMenuView()
        {
            IEnumerable<ILevelView> levelViews = _viewBuilder.BuildLevelViews(_levelContainer, this);
            IEnumerable<ILevelStageView> levelStageViews = _viewBuilder.BuildStageLevelViews(_stageContainer, this);

            _selectableLevelsGroup.Initialize(levelViews);
            _selectableLevelStagesGroup.Initialize(levelStageViews);
        }   
        
        public void RebuildLevelStageViews()
        {
            IEnumerable<ILevelStageView> levelStageViews = _viewBuilder.BuildStageLevelViews(_stageContainer, this);

            _selectableLevelStagesGroup.Initialize(levelStageViews);
        }

        public int GetSelectedLevelId() => _levelService.SelectedLevelId;
        public int GetSelectedLevelStageId() => _levelService.SelectedLevelStageId;
        public int GetMaxStageId() => _levelService.SelectedLevelMaxStageId;
        public void SelectLevel(int id) => _levelService.SelectLevel(id);
        public void SelectLevelStage(int id) => _levelService.SelectLevelStage(id);
        public void StartLevel() => _levelService.StartLevel();
        public void CloseLevel() => _levelService.CloseLevel();

        public void StartGameLoop()
        {
            Debug.Log("GameLoop ready");
        }

        public void SelectNextStage() => _levelService.SelectNextStage();
        public void SelectPreviousStage() => _levelService.SelectPreviousStage();
    }
}