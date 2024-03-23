using System;
using System.Linq;
using Source.Application.Configs;
using Source.Controllers.Api.Services;

namespace Source.Controllers.Core.Services
{
    public class LevelService : ILevelService
    {
        private readonly LevelViewSo _levelViewSo;

        public LevelService(LevelViewSo levelViewSo)
        {
            _levelViewSo = levelViewSo;
        }

        public event Action<int> LevelSelected;
        public event Action<int> LevelStageSelected;
        public event Action<int> LevelStarted;
        public event Action<int> LevelClosed;
        public int SelectedLevelId { get; private set; }
        public int SelectedLevelStageId { get; private set; }
        public int SelectedLevelMaxStageId { get; private set; }

        public bool IsNextStageAvailable =>
            SelectedLevelMaxStageId > 0 && SelectedLevelStageId < SelectedLevelMaxStageId - 1;

        public bool IsPreviousStageAvailable => SelectedLevelStageId > 0;

        public void SelectLevel(int levelId)
        {
            SelectedLevelId = levelId;
            SelectedLevelMaxStageId = _levelViewSo
                .LevelViewsData
                .First(data => data.Order == levelId)
                .StageVariantsData
                .Length;

            SelectLevelStage(0);
            LevelSelected?.Invoke(SelectedLevelId);
        }

        public void SelectLevelStage(int levelId)
        {
            SelectedLevelStageId = levelId;
            LevelStageSelected?.Invoke(SelectedLevelStageId);
        }

        public void SelectNextStage() =>
            SelectLevelStage(SelectedLevelStageId + 1);

        public void SelectPreviousStage() =>
            SelectLevelStage(SelectedLevelStageId - 1);

        public void StartLevel() =>
            LevelStarted?.Invoke(SelectedLevelId);

        public void CloseLevel() =>
            LevelClosed?.Invoke(SelectedLevelId);

    }
}