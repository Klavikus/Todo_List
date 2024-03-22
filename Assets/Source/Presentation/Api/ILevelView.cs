using Assets.Source.Common.Components.Interfaces;
using Source.Controllers.Api.Mediators;
using UnityEngine;

namespace Source.Presentation.Api
{
    public interface ILevelView : ISelectable
    {
        void Initialize(IUIMediator iuiMediator, int order, Sprite icon);
    }
}