using System;
using Assets.Source.Common.WindowFsm;
using Source.Controllers.Api.Services;
using Source.Controllers.Api.ViewModels;
using Source.Infrastructure.Api.Builders;
using Sources.Infrastructure.Api.GameFsm;
using UnityEngine;

namespace Source.Controllers.Core.ViewModels
{
    public class LevelSelectionViewModel : ILevelSelectionViewModel, IDisposable
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentDataService _persistentDataService;
        private readonly IViewBuilder _viewBuilder;

        public LevelSelectionViewModel
        (
            IWindowFsm windowFsm,
            IGameStateMachine gameStateMachine,
            IPersistentDataService persistentDataService,
            IViewBuilder viewBuilder
        )
        {
            _windowFsm = windowFsm;
            _gameStateMachine = gameStateMachine;
            _persistentDataService = persistentDataService;
            _viewBuilder = viewBuilder;
        }

        public void StartGameLoop() =>
            _gameStateMachine.GoToGameLoop();

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void BuildLevelViews(Transform levelViewContainer)
        {
        }

        public void BuildLevelStageViews(Transform levelStageViewContainer)
        {
        }
    }
}