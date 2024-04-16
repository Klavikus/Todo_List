using System;
using System.Collections.Generic;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
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
using Source.Presentation.Core;
using UnityEngine;
using Zenject;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Application.CompositionRoots
{
    public class MainMenuCompositionRoot : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainTaskListView _mainTaskListView;
        [SerializeField] private TaskCreationView _taskCreationView;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private ConfigurationContainer _configurationContainer;

        [Inject]
        public async void Initialize(IProgressRepository repository)
        {
            // Type[] dataTypes = {typeof(TaskData)};
            // IData gameData = new GameData(dataTypes);
            // IDataContext dataContext = new JsonPrefsDataContext(gameData, "JsonData");
            // IProgressRepository repository = new CompositeRepository(dataContext, dataTypes);

            await repository.Load();

            Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>()
            {
                [typeof(RootWindow)] = new RootWindow(),
                [typeof(MainTaskListWindow)] = new MainTaskListWindow(),
                [typeof(TaskCreationWindow)] = new TaskCreationWindow(),
                [typeof(TaskWindow)] = new TaskWindow(),
            };

            IWindowFsm windowFsm = new WindowFsm<RootWindow>(windows);

            ILogger logger = new DebugLogger();
            ITaskService taskService = new TaskService(repository);

            TaskPresenterFactory taskPresenterFactory = new();

            TaskViewFactory taskViewFactory = new(_configurationContainer.CreatedTaskViewPrefab,
                windowFsm, logger,
                taskService, taskPresenterFactory);

            MainMenuPresenter mainMenuPresenter = new(_mainMenuView, windowFsm, logger, taskService);
            MainTaskListPresenter mainTaskListPresenter = new(_mainTaskListView, windowFsm, logger, taskService,
                (data, container) => taskViewFactory.Create(data, container));
            TaskCreationPresenter taskCreationPresenter = new(_taskCreationView, windowFsm, logger, taskService);
            TaskViewPresenter taskViewPresenter = new(_taskView, windowFsm, logger, taskService);

            _mainMenuView.Construct(mainMenuPresenter);
            _mainTaskListView.Construct(mainTaskListPresenter);
            _taskCreationView.Construct(taskCreationPresenter);
            _taskView.Construct(taskViewPresenter);
        }
    }
}