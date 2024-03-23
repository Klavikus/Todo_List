using System;
using System.Collections.Generic;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Controllers.Core.Presenters;
using Source.Controllers.Core.WindowFsms;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Infrastructure.Core.Services;
using Source.Infrastructure.Core.Services.DI;
using Source.Presentation.Core;
using UnityEngine;
using ILogger = Sources.Infrastructure.Api.Services.ILogger;

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
                [typeof(MainTaskListWindow)] = new MainTaskListWindow(),
                [typeof(TaskCreationWindow)] = new TaskCreationWindow(),
                [typeof(TaskWindow)] = new TaskWindow(),
            };

            IWindowFsm windowFsm = new WindowFsm<RootWindow>(windows);
            // IConfigurationProvider configurationProvider = serviceContainer.Single<IConfigurationProvider>();
            // IPersistentDataService persistentDataService = serviceContainer.Single<IPersistentDataService>();

            ILogger logger = new DebugLogger();

            MainMenuPresenter mainMenuPresenter = new MainMenuPresenter(_mainMenuView, windowFsm, logger);
            MainTaskListPresenter mainTaskListPresenter =
                new MainTaskListPresenter(_mainTaskListView, windowFsm, logger);
            TaskCreationPresenter taskCreationPresenter =
                new TaskCreationPresenter(_taskCreationView, windowFsm, logger);
            TaskViewPresenter taskViewPresenter = new TaskViewPresenter(_taskView, windowFsm, logger);

            _mainMenuView.Construct(mainMenuPresenter);
            _mainTaskListView.Construct(mainTaskListPresenter);
            _taskCreationView.Construct(taskCreationPresenter);
            _taskView.Construct(taskViewPresenter);
        }
    }
}