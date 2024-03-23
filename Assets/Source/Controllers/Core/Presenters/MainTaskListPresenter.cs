using System;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Sources.Infrastructure.Api.Services;

namespace Source.Controllers.Core.Presenters
{
    public class MainTaskListPresenter : IPresenter
    {
        private readonly IMainTaskListView _mainTaskListView;
        private readonly IWindowFsm _windowFsm;

        public MainTaskListPresenter(IMainTaskListView mainTaskListView, IWindowFsm windowFsm, ILogger logger)
        {
            _mainTaskListView = mainTaskListView ?? throw new ArgumentNullException(nameof(mainTaskListView));
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
            if (window is MainTaskListWindow)
                _mainTaskListView.Show();
            else
                _mainTaskListView.Hide();
        }
    }
}