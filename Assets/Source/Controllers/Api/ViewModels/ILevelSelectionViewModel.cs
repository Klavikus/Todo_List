using UnityEngine;

namespace Source.Controllers.Api.ViewModels
{
    public interface ILevelSelectionViewModel
    {
        void BuildLevelViews(Transform levelViewContainer);
        void BuildLevelStageViews(Transform levelStageViewContainer);
    }
}