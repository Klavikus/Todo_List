using System;
using Modules.DAL.Implementation.Data.Entities;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Factories;
using Source.Controllers.Api.Services;
using Source.Presentation.Api.Factories;
using Source.Presentation.Api.Views;
using Source.Presentation.Core.Configs;
using Source.Presentation.Core.Views;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;
using Object = UnityEngine.Object;

namespace Source.Presentation.Core.Factories
{
    public class CreatedTaskViewFactory : ICreatedTaskViewFactory
    {
        private readonly CreatedTaskView _viewPrefab;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly ITaskPresenterFactory<ICreatedTaskView> _taskPresenterFactory;

        public CreatedTaskViewFactory(
            PresentationConfiguration presentationConfiguration,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            ITaskPresenterFactory<ICreatedTaskView> taskPresenterFactory)
        {
            _viewPrefab = presentationConfiguration.CreatedTaskViewPrefab;
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskPresenterFactory =
                taskPresenterFactory ?? throw new ArgumentNullException(nameof(taskPresenterFactory));
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