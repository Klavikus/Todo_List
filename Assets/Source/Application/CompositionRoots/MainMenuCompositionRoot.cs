using System;
using System.Collections.Generic;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Source.Application.Factories;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Controllers.Core.Services;
using Source.Controllers.Core.WindowFsms;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Infrastructure.Core;
using Source.Infrastructure.Core.Services;
using Source.Infrastructure.Core.Services.DI;
using Source.Presentation.Core;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Application.CompositionRoots
{
    public sealed class MainMenuCompositionRoot : SceneCompositionRoot
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainTaskListView _mainTaskListView;
        [SerializeField] private TaskCreationView _taskCreationView;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private ConfigurationContainer _configurationContainer;

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
            dataContext.Clear();

            for (int i = 0; i < 10; i++)
            {
                TaskData task = new($"{nameof(TaskData)}_{i}")
                {
                    Name = $"Task №{i}",
                    Description = $"This is test task",
                    TargetDate = DateTime.Now.Date,
                    IsCompleted = i % 2 == 0
                };
                repository.Add<TaskData>(task);
            }

            await dataContext.Save();

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

            TaskPresenterFactory taskPresenterFactory = new TaskPresenterFactory();

            TaskViewFactory taskViewFactory = new TaskViewFactory(_configurationContainer.CreatedTaskViewPrefab,
                windowFsm, logger,
                taskService, taskPresenterFactory);

            MainMenuPresenter mainMenuPresenter =
                new MainMenuPresenter(_mainMenuView, windowFsm, logger, taskService);
            MainTaskListPresenter mainTaskListPresenter =
                new MainTaskListPresenter(_mainTaskListView, windowFsm, logger, taskService,
                    (data, container) => taskViewFactory.Create(data, container));
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