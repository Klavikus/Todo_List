using System;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Services;
using Source.Controllers.Api.ViewModels;
using Source.Infrastructure.Api.GameFsm;

namespace Source.Controllers.Core.ViewModels
{
    public class MainMenuViewModel : IMainMenuViewModel, IDisposable
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentDataService _persistentDataService;

        public MainMenuViewModel
        (
            IWindowFsm windowFsm,
            IGameStateMachine gameStateMachine,
            IPersistentDataService persistentDataService
        )
        {
            _windowFsm = windowFsm;
            _gameStateMachine = gameStateMachine;
            _persistentDataService = persistentDataService;
        }

        public void StartGameLoop() =>
            _gameStateMachine.GoToGameLoop();

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}