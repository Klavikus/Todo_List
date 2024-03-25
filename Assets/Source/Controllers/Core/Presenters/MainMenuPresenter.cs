using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DeadMosquito.AndroidGoodies;
using Modules.DAL.Implementation.Data;
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
        private readonly IMainMenuView _mainMenuView;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly string _todayTasksPrefix = "Задач на сегодня: ";

        public MainMenuPresenter(
            IMainMenuView mainMenuView,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService)
        {
            _mainMenuView = mainMenuView ?? throw new ArgumentNullException(nameof(mainMenuView));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        public void Enable()
        {
            _mainMenuView.ViewTasksButton.Initialize();
            _mainMenuView.CreateTasksButton.Initialize();

            OnWindowOpened(_windowFsm.CurrentWindow);
            _windowFsm.Opened += OnWindowOpened;

            _mainMenuView.SetCurrentDateText(DateTime.Now.ToShortDateString());
            _mainMenuView.CreateTasksButton.Clicked += OnCreateTasksButtonClicked;
            _mainMenuView.ViewTasksButton.Clicked += OnViewTasksButtonClicked;

            _taskService.TaskCreated += UpdateTaskCounter;

            _taskService.FocusDate(DateTime.Now.Date);
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
            _mainMenuView.CreateTasksButton.Clicked -= OnCreateTasksButtonClicked;
            _mainMenuView.ViewTasksButton.Clicked -= OnViewTasksButtonClicked;
            _taskService.TaskCreated -= UpdateTaskCounter;
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
                UpdateTaskCounter(null);
                _mainMenuView.Show();
                _windowFsm.ClearHistory();
            }
            else
            {
                _mainMenuView.Hide();
            }
        }

        private void UpdateTaskCounter(TaskData _)
        {
            IEnumerable<TaskData> todayTasks = _taskService.GetTodayTasks();
            _mainMenuView.SetTodayTasksText(_todayTasksPrefix + todayTasks.Count());
        }
    }
}