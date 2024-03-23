using System;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Sources.Infrastructure.Api.Services;

namespace Source.Controllers.Core.Presenters
{
    public class TaskCreationPresenter : IPresenter
    {
        private readonly ITaskCreationView _taskCreationView;
        private readonly IWindowFsm _windowFsm;

        public TaskCreationPresenter(ITaskCreationView taskCreationView, IWindowFsm windowFsm, ILogger logger)
        {
            _taskCreationView = taskCreationView ?? throw new ArgumentNullException(nameof(taskCreationView));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
        }

        public void Enable()
        {
            OnWindowOpened(_windowFsm.CurrentWindow);

            _windowFsm.Opened += OnWindowOpened;
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
        }

        private void OnWindowOpened(IWindow window)
        {
            if (window is TaskCreationWindow)
                _taskCreationView.Show();
            else
                _taskCreationView.Hide();
        }
    }
}