using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DeadMosquito.AndroidGoodies;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;

namespace Source.Controllers.Core.Presenters
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly IMainMenuView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly string _todayTasksPrefix = "Today tasks: ";

        public MainMenuPresenter(
            IMainMenuView view,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        public void Enable()
        {
            _view.ViewTasksButton.Initialize();
            _view.CreateTasksButton.Initialize();

            OnWindowOpened(_windowFsm.CurrentWindow);
            _windowFsm.Opened += OnWindowOpened;

            UpdateTodayTaskCounter(null);
            _view.CreateTasksButton.Clicked += OnCreateTasksButtonClicked;
            _view.ViewTasksButton.Clicked += OnViewTasksButtonClicked;

            _taskService.TaskCreated += UpdateTodayTaskCounter;
            _taskService.FocusedDateChanged += OnFocusedDateChanged;

            _view.CrButton.onClick.AddListener(OnCreateTasksButtonClicked);
            
            _taskService.FocusDate(DateTime.Now.Date);
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
            _view.CreateTasksButton.Clicked -= OnCreateTasksButtonClicked;
            _view.ViewTasksButton.Clicked -= OnViewTasksButtonClicked;
            _taskService.TaskCreated -= UpdateTodayTaskCounter;
            _taskService.FocusedDateChanged -= OnFocusedDateChanged;
            _view.CrButton.onClick.RemoveListener(OnCreateTasksButtonClicked);

        }

        private void OnViewTasksButtonClicked()
        {
            _windowFsm.OpenWindow<MainTaskListWindow>();
        }

        private void OnCreateTasksButtonClicked() =>
            _windowFsm.OpenWindow<TaskCreationWindow>();

        private void OnWindowOpened(IWindow window)
        {
            if (window is RootWindow)
            {
                UpdateTodayTaskCounter(null);
                UpdateAllTaskCounter();
                _view.Show();
                _windowFsm.ClearHistory();
            }
            else
            {
                _view.Hide();
            }
        }

        private void UpdateAllTaskCounter()
        {
            TaskData[] tasks = _taskService.GetAllTasks().ToArray();

            _view.AllTasksText.text = $"Tasks: {tasks.Length}";
            _view.AllCompletedText.text = $"Completed: {tasks.Count(task => task.IsCompleted)}";
            _view.AllInProgressText.text = $"In progress: {tasks.Count(task => task.IsCompleted == false)}";
        }

        private void UpdateTodayTaskCounter(TaskData _)
        {
            TaskData[] todayTasks = _taskService.GetTodayTasks().ToArray();

            _view.CurrentDateText.text = DateTime.Now.ToShortDateString();
            _view.TodayTasksText.text = _todayTasksPrefix + todayTasks.Length;
            _view.TodayCompletedTasksText.text = $"Completed: {todayTasks.Count(task => task.IsCompleted)}";
            _view.TodayInProgressTasksText.text = $"In progress: {todayTasks.Count(task => task.IsCompleted == false)}";
        }

        private void OnFocusedDateChanged(DateTime dateTime)
        {
            if (dateTime.Date != DateTime.Now.Date)
                return;

            UpdateTodayTaskCounter(null);
        }
    }
}