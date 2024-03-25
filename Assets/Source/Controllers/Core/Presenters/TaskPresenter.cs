using System;
using Modules.DAL.Implementation.Data;
using Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Controllers.Core.Presenters
{
    public class TaskPresenter : IPresenter
    {
        private readonly ICreatedTaskView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly TaskData _taskData;

        public TaskPresenter(
            ICreatedTaskView view,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            TaskData taskData)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskData = taskData;
        }

        public void Enable()
        {
            _view.OpenViewButton.Initialize();
            _view.Name.text = _taskData.Name;
            _view.StatusImage.color = _taskData.IsCompleted ? Color.green : Color.red;
            _view.OpenViewButton.Clicked += OnOpenViewButtonClicked;
            _taskService.TaskChanged += OnTaskChanged;
        }

        private void OnTaskChanged(TaskData taskData)
        {
            if (taskData.Id != _taskData.Id)
                return;
            
            _view.Name.text = _taskData.Name;
            _view.StatusImage.color = _taskData.IsCompleted ? Color.green : Color.red;
        }

        public void Disable()
        {
            _view.OpenViewButton.Clicked -= OnOpenViewButtonClicked;
            _taskService.TaskChanged -= OnTaskChanged;
        }

        private void OnOpenViewButtonClicked()
        {
            _taskService.FocusTask(_taskData);
            _windowFsm.OpenWindow<TaskWindow>();
        }
    }
}