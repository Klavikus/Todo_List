using Assets.Source.Common.Components.Interfaces;

namespace Source.Presentation.Api
{
    using Source.Controllers.Api.Mediators;
    using UnityEngine;

    public interface ILevelStageView : ISelectable
    {
        void Initialize(IUIMediator mediator, Sprite icon, int order);
    }
}