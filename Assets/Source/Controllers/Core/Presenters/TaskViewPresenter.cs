using System;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Sources.Infrastructure.Api.Services;

namespace Source.Controllers.Core.Presenters
{
    public class TaskViewPresenter : IPresenter
    {
        private readonly ITaskView _taskView;
        private readonly IWindowFsm _windowFsm;

        public TaskViewPresenter(ITaskView taskView, IWindowFsm windowFsm, ILogger logger)
        {
            _taskView = taskView ?? throw new ArgumentNullException(nameof(taskView));
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
            if (window is TaskWindow)
                _taskView.Show();
            else
                _taskView.Hide();
        }
    }
}