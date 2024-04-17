using System;
using Modules.DAL.Implementation.Data.Entities;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Source.Presentation.Api.Views;

namespace Source.Controllers.Core.Presenters
{
    public class TaskViewPresenter : IPresenter
    {
        private readonly ITaskView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;

        public TaskViewPresenter(ITaskView view, IWindowFsm windowFsm, ILogger logger,
            ITaskService taskService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        public void Enable()
        {
            _view.ExitButton.Initialize();
            _view.CompleteButton.Initialize();
            _view.DeleteButton.Initialize();

            _view.ExitButton.Clicked += OnExitButtonClicked;
            _view.CompleteButton.Clicked += OnCompleteButtonClicked;
            _view.DeleteButton.Clicked += OnDeleteButtonClicked;

            OnWindowOpened(_windowFsm.CurrentWindow);

            _windowFsm.Opened += OnWindowOpened;

            _taskService.TaskChanged += OnTaskChanged;
        }

        public void Disable()
        {
            _view.ExitButton.Clicked -= OnExitButtonClicked;
            _view.CompleteButton.Clicked -= OnCompleteButtonClicked;
            _windowFsm.Opened -= OnWindowOpened;
            _taskService.TaskChanged -= OnTaskChanged;
        }

        private void OnWindowOpened(IWindow window)
        {
            if (window is TaskWindow)
            {
                UpdateView();
                _view.Show();
            }
            else
                _view.Hide();
        }

        private void OnExitButtonClicked()
        {
            _windowFsm.Close<TaskWindow>();
        }

        private void OnCompleteButtonClicked()
        {
            if (_taskService.FocusedTask.IsCompleted)
            {
                _taskService.ResetTask();
            }
            else
            {
                _taskService.CompleteTask();
            }
        }

        private void UpdateView()
        {
            TaskData taskData = _taskService.FocusedTask;
            _view.CurrentDateText.text = taskData.TargetDate.ToShortDateString();
            _view.NameText.text = taskData.Name;
            _view.TaskDescriptionText.text = taskData.Description;
            _view.CompletionText.text = taskData.IsCompleted ? "Reset" : "Complete";
            _view.SetCompletionStatus(taskData.IsCompleted);
        }

        private void OnTaskChanged(TaskData taskData) =>
            UpdateView();

        private void OnDeleteButtonClicked()
        {
            _taskService.DeleteTask(_taskService.FocusedTask);
            _windowFsm.Close<TaskWindow>();
        }
    }
}