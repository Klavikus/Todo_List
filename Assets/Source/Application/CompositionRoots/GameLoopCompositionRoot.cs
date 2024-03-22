using System;
using System.Collections.Generic;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Infrastructure.Core.Services.DI;
using Source.Presentation.Core;
using Sources.Controllers.Core.WindowFsms;
using Sources.Controllers.Core.WindowFsms.Windows;
using Sources.Infrastructure.Api.Services;
using Sources.Infrastructure.Api.Services.Providers;
using UnityEngine;

namespace Sources.Application.CompositionRoots
{
    public sealed class GameLoopCompositionRoot : SceneCompositionRoot, ICoroutineRunner
    {
        [SerializeField] private GameLoopView _gameLoopView;

        public override void Initialize(ServiceContainer serviceContainer)
        {
            IConfigurationProvider configurationProvider = serviceContainer.Single<IConfigurationProvider>();

            Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>()
            {
                [typeof(RootWindow)] = new RootWindow(),
                [typeof(GameLoopWindow)] = new GameLoopWindow(),
            };

            IWindowFsm windowFsm = new WindowFsm<RootWindow>(windows);
        }
    }
}