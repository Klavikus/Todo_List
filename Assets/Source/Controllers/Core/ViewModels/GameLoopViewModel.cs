using System;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Services;
using Source.Controllers.Api.ViewModels;
using Source.Infrastructure.Api.GameFsm;

namespace Source.Controllers.Core.ViewModels
{
    public class GameLoopViewModel : IGameLoopViewModel, IDisposable
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameLoopService _gameLoopService;

        public GameLoopViewModel
        (
            IWindowFsm windowFsm,
            IGameStateMachine gameStateMachine,
            IGameLoopService gameLoopService
        )
        {
            _windowFsm = windowFsm;
            _gameStateMachine = gameStateMachine;
            _gameLoopService = gameLoopService;
        }

        public void GoToMainMenu() => 
            _gameStateMachine.GoToMainMenu();

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}