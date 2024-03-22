using System;
using System.Collections.Generic;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Mediators;
using Source.Infrastructure.Api;
using Source.Infrastructure.Core.Builders;
using Source.Infrastructure.Core.Services.DI;
using Source.Presentation.Core;
using Source.Presentation.Core.LevelSelection;
using Sources.Controllers.Core.Services;
using Sources.Controllers.Core.WindowFsms;
using Sources.Controllers.Core.WindowFsms.Windows;
using Sources.Infrastructure.Api.GameFsm;
using Sources.Infrastructure.Api.Services.Providers;
using UnityEngine;

namespace Sources.Application.CompositionRoots
{
    public sealed class MainMenuCompositionRoot : SceneCompositionRoot
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainTaskListView _mainTaskListView;
        [SerializeField] private TaskCreationView _taskCreationView;
        [SerializeField] private TaskView _taskView;

        private void Awake()
        {
            Initialize(new ServiceContainer());
        }

        public override void Initialize(ServiceContainer serviceContainer)
        {
            Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>()
            {
                [typeof(RootWindow)] = new RootWindow(),
            };

            IWindowFsm windowFsm = new WindowFsm<RootWindow>(windows);
            // IConfigurationProvider configurationProvider = serviceContainer.Single<IConfigurationProvider>();
            // IPersistentDataService persistentDataService = serviceContainer.Single<IPersistentDataService>();


            _mainMenuView.Initialize();
            _mainTaskListView.Initialize();
            _taskCreationView.Initialize();
            _taskView.Initialize();
        }
    }
}