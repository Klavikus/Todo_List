using System;
using JetBrains.Annotations;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Infrastructure.Api.Services.Providers;
using Source.Infrastructure.Core;
using Source.Presentation.Api;
using Source.Presentation.Core;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;
using Object = UnityEngine.Object;

namespace Source.Application.Factories
{
    public class TaskViewFactory : ICreatedTaskViewFactory
    {
        private readonly CreatedTaskView _viewPrefab;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly ITaskPresenterFactory _taskPresenterFactory;

        public TaskViewFactory(
            ConfigurationContainer configurationContainer,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            ITaskPresenterFactory taskPresenterFactory)
        {
            if (configurationContainer == null)
                throw new ArgumentNullException(nameof(configurationContainer));

            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskPresenterFactory = taskPresenterFactory ?? throw new ArgumentNullException(nameof(taskPresenterFactory));
         
            _viewPrefab = configurationContainer.CreatedTaskViewPrefab;
        }

        public ICreatedTaskView Create(TaskData taskData, Transform parentContainer)
        {
            CreatedTaskView view = Object.Instantiate(_viewPrefab, parentContainer);

            IPresenter presenter = _taskPresenterFactory.Create(view, _windowFsm, _logger, _taskService, taskData);
            view.Construct(presenter);

            return view;
        }
    }
}