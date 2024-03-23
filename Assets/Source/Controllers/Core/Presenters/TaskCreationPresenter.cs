using System;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Infrastructure.Api.Services;
using Source.Presentation.Api;

namespace Source.Controllers.Core.Presenters
{
    public class TaskCreationPresenter : IPresenter
    {
        private readonly ITaskCreationView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;

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
            _view.ApplyTaskButton.Initialize();
            _view.ExitTasksButton.Initialize();
            _view.SelectDateButton.Initialize();

            OnWindowOpened(_windowFsm.CurrentWindow);
            _windowFsm.Opened += OnWindowOpened;

            _view.ExitTasksButton.Clicked += OnExitTasksButtonClicked;
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
            _view.ExitTasksButton.Clicked -= OnExitTasksButtonClicked;
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
    }
}