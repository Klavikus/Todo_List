using System.Collections.Generic;
using System.Linq;
using Source.Application.Configs;
using Source.Controllers.Api.Mediators;
using Source.Domain.Data;
using Source.Infrastructure.Api;
using Source.Infrastructure.Api.Builders;
using Source.Presentation.Api;
using Source.Presentation.Core.LevelSelection;
using UnityEngine;

namespace Source.Infrastructure.Core.Builders
{
    public class ViewBuilder : IViewBuilder
    {
        private readonly LevelViewSo _levelViewConfig;
        private readonly IResourceProvider _resourceProvider;

        public ViewBuilder(LevelViewSo levelViewConfig, IResourceProvider resourceProvider)
        {
            _levelViewConfig = levelViewConfig;
            _resourceProvider = resourceProvider;
        }

        public IEnumerable<ILevelView> BuildLevelViews(Transform container, IUIMediator mediator)
        {
            DestroyChildren(container);

            LevelView levelViewTemplate = _resourceProvider.Load<LevelView>();

            IOrderedEnumerable<LevelViewData> levelViewsData =
                _levelViewConfig.LevelViewsData.OrderBy(data => data.Order);

            List<ILevelView> result = new List<ILevelView>();

            foreach (LevelViewData levelViewData in levelViewsData)
            {
                LevelView levelView = Object.Instantiate(levelViewTemplate, container);
                levelView.Initialize(mediator, levelViewData.Order, levelViewData.Icon);
                result.Add(levelView);
            }

            return result;
        }

        public IEnumerable<ILevelStageView> BuildStageLevelViews(Transform container, IUIMediator iuiMediator)
        {
            DestroyChildren(container);

            LevelStageView levelStageViewTemplate = _resourceProvider.Load<LevelStageView>();

            LevelViewData levelViewData = _levelViewConfig
                .LevelViewsData
                .OrderBy(data => data.Order)
                .ToArray()[iuiMediator.GetSelectedLevelId()];

            List<ILevelStageView> result = new List<ILevelStageView>();

            for (int i = 0; i < levelViewData.StageVariantsData.Length; i++)
            {
                StageVariantData variantData = levelViewData.StageVariantsData[i];
                LevelStageView stageView = Object.Instantiate(levelStageViewTemplate, container);
                stageView.Initialize(iuiMediator, variantData.Icon, i);
                result.Add(stageView);
            }

            return result;
        }

        private static void DestroyChildren(Transform container)
        {
            for (int i = 0; i < container.childCount; i++)
                Object.Destroy(container.GetChild(i).gameObject);
        }
    }
}