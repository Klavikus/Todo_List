using System;
using JetBrains.Annotations;
using Modules.DAL.Implementation.Data;
using Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Presentation.Api;
using Source.Presentation.Core;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;
using Object = UnityEngine.Object;

namespace Source.Application.Factories
{
    public class TaskViewFactory : ICreatedTaskViewFactory
    {
        [NotNull] private readonly CreatedTaskView _viewPrefab;
        [NotNull] private readonly IWindowFsm _windowFsm;
        [NotNull] private readonly ILogger _logger;
        [NotNull] private readonly ITaskService _taskService;
        private readonly ITaskPresenterFactory _taskPresenterFactory;

        public TaskViewFactory(
            CreatedTaskView viewPrefab,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            ITaskPresenterFactory taskPresenterFactory)
        {
            _viewPrefab = viewPrefab ? viewPrefab : throw new ArgumentNullException(nameof(viewPrefab));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskPresenterFactory =
                taskPresenterFactory ?? throw new ArgumentNullException(nameof(taskPresenterFactory));
        }

        public ICreatedTaskView Create(TaskData taskData, Transform parentContainer)
        {
            CreatedTaskView view = Object.Instantiate(_viewPrefab, parentContainer);

            IPresenter presenter = _taskPresenterFactory.Create(view, _windowFsm, _logger, _taskService);
            view.Construct(presenter);

            return view;
        }
    }
}