using System;
using System.Collections.Generic;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Controllers.Core.Services;
using Source.Controllers.Core.WindowFsms;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Infrastructure.Core.Services;
using Source.Infrastructure.Core.Services.DI;
using Source.Presentation.Core;
using UnityEngine;
using ILogger = Source.Infrastructure.Api.Services.ILogger;

namespace Source.Application.CompositionRoots
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

        public override async void Initialize(ServiceContainer serviceContainer)
        {
            Type[] dataTypes = {typeof(TaskData)};
            IData gameData = new GameData(dataTypes);
            IDataContext dataContext = new JsonPrefsDataContext(gameData, "JsonData");
            IProgressRepository repository = new CompositeRepository(dataContext, dataTypes);

            await dataContext.Load();

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
            ITaskService taskService = new TaskService(repository);

            // windowFsm.Opened += (window) => logger.Log("Opened " + window);
            // windowFsm.Closed += (window) => logger.Log("Closed " + window);

            MainMenuPresenter mainMenuPresenter =
                new MainMenuPresenter(_mainMenuView, windowFsm, logger, taskService);
            MainTaskListPresenter mainTaskListPresenter =
                new MainTaskListPresenter(_mainTaskListView, windowFsm, logger, taskService);
            TaskCreationPresenter taskCreationPresenter =
                new TaskCreationPresenter(_taskCreationView, windowFsm, logger, taskService);
            TaskViewPresenter taskViewPresenter =
                new TaskViewPresenter(_taskView, windowFsm, logger, taskService);

            _mainMenuView.Construct(mainMenuPresenter);
            _mainTaskListView.Construct(mainTaskListPresenter);
            _taskCreationView.Construct(taskCreationPresenter);
            _taskView.Construct(taskViewPresenter);
        }
    }
}