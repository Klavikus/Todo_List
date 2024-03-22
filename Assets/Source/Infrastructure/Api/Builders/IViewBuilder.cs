using System.Collections.Generic;
using Source.Controllers.Api.Mediators;
using Source.Presentation.Api;
using UnityEngine;

namespace Source.Infrastructure.Api.Builders
{
    public interface IViewBuilder
    {
        IEnumerable<ILevelView> BuildLevelViews(Transform container, IUIMediator iuiMediator);
        IEnumerable<ILevelStageView> BuildStageLevelViews(Transform stageContainer, IUIMediator iuiMediator);
    }
}