using System;
using DeadMosquito.AndroidGoodies;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Source.Presentation.Api.Views;

namespace Source.Controllers.Core.Presenters
{
    public class TaskCreationPresenter : IPresenter
    {
        private readonly ITaskCreationView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;

        private DateTime _currentDateTime;

        public TaskCreationPresenter(
            ITaskCreationView view,
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
            _currentDateTime = DateTime.Now.Date;

            _view.ApplyTaskButton.Initialize();
            _view.ExitTasksButton.Initialize();
            _view.SelectDateButton.Initialize();

            OnWindowOpened(_windowFsm.CurrentWindow);
            OnFocusedDateChanged(_taskService.FocusedDate);
            _windowFsm.Opened += OnWindowOpened;

            _view.ApplyTaskButton.Clicked += OnApplyTasksButtonClicked;
            _view.ExitTasksButton.Clicked += OnExitTasksButtonClicked;
            _view.SelectDateButton.Clicked += OnSelectDateButtonClicked;
            _taskService.FocusedDateChanged += OnFocusedDateChanged;
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
            _view.ApplyTaskButton.Clicked -= OnApplyTasksButtonClicked;
            _view.ExitTasksButton.Clicked -= OnExitTasksButtonClicked;
            _view.SelectDateButton.Clicked -= OnSelectDateButtonClicked;
            _taskService.FocusedDateChanged -= OnFocusedDateChanged;
        }

        private void OnWindowOpened(IWindow window)
        {
            if (window is TaskCreationWindow)
                _view.Show();
            else
                _view.Hide();
        }

        private void OnExitTasksButtonClicked() =>
            _windowFsm.Close<TaskCreationWindow>();

        private void OnApplyTasksButtonClicked()
        {
            _taskService.CreateTask(_view.TaskNameInputField.text, _view.TaskDescriptionInputField.text);
            _windowFsm.Close<TaskCreationWindow>();
        }

        private void OnSelectDateButtonClicked()
        {
            AGDateTimePicker.ShowDatePicker(
                _currentDateTime.Year,
                _currentDateTime.Month,
                _currentDateTime.Day,
                OnDatePicked,
                OnDatePickCanceled);
        }

        private void OnDatePicked(int year, int month, int day) =>
            _taskService.FocusDate(new DateTime(year, month, day).Date);

        private void OnDatePickCanceled() =>
            _logger.LogWarning($"{nameof(MainMenuPresenter)}: {nameof(OnDatePickCanceled)}");

        private void OnFocusedDateChanged(DateTime dateTime) =>
            _view.SetSelectedDateText(dateTime.ToShortDateString());
    }
}