using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.DAL.Implementation.Data;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Controllers.Core.Presenters
{
    public class MainTaskListPresenter : IPresenter
    {
        private readonly IMainTaskListView _mainTaskListView;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly Func<TaskData, Transform, ICreatedTaskView> _createdTaskViewStrategy;

        public MainTaskListPresenter(IMainTaskListView mainTaskListView, IWindowFsm windowFsm, ILogger logger,
            ITaskService taskService, Func<TaskData, Transform, ICreatedTaskView> createdTaskViewStrategy)
        {
            _mainTaskListView = mainTaskListView ?? throw new ArgumentNullException(nameof(mainTaskListView));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _createdTaskViewStrategy =
                createdTaskViewStrategy ?? throw new ArgumentNullException(nameof(createdTaskViewStrategy));
        }

        public void Enable()
        {
            _mainTaskListView.SelectDateButton.Initialize();
            _mainTaskListView.CreateTasksButton.Initialize();
            _mainTaskListView.ExitTasksButton.Initialize();

            _mainTaskListView.ExitTasksButton.Clicked += OnExitTasksButtonClicked;
            _mainTaskListView.CreateTasksButton.Clicked += OnCreateTasksButtonClicked;

            OnWindowOpened(_windowFsm.CurrentWindow);
            _windowFsm.Opened += OnWindowOpened;

            OnFocusedDateChanged(_taskService.FocusedDate);
            _taskService.FocusedDateChanged += OnFocusedDateChanged;
            _taskService.TaskChanged += (_) => RebuildCreatedTasksList();
        }

        public void Disable()
        {
            _mainTaskListView.ExitTasksButton.Clicked -= OnExitTasksButtonClicked;
            _mainTaskListView.CreateTasksButton.Clicked -= OnCreateTasksButtonClicked;
            _windowFsm.Opened -= OnWindowOpened;
            _taskService.FocusedDateChanged -= OnFocusedDateChanged;
        }

        private void OnWindowOpened(IWindow window)
        {
            if (window is MainTaskListWindow)
                _mainTaskListView.Show();
            else
                _mainTaskListView.Hide();
        }

        private void OnExitTasksButtonClicked() =>
            _windowFsm.Close<MainTaskListWindow>();

        private void OnFocusedDateChanged(DateTime dateTime)
        {
            _mainTaskListView.SetSelectedDateText(dateTime.ToShortDateString());
            RebuildCreatedTasksList();
        }

        private void OnCreateTasksButtonClicked() =>
            _windowFsm.OpenWindow<TaskCreationWindow>();

        private void RebuildCreatedTasksList()
        {
            IEnumerable<TaskData> createdTasks = _taskService.GetTodayTasks();

            foreach (ICreatedTaskView taskCreationView in _mainTaskListView.CreatedTaskContainer
                         .GetComponentsInChildren<ICreatedTaskView>(true))
                taskCreationView.Destroy();

            foreach (TaskData taskData in createdTasks)
                _createdTaskViewStrategy.Invoke(taskData, _mainTaskListView.CreatedTaskContainer);
        }
    }
}