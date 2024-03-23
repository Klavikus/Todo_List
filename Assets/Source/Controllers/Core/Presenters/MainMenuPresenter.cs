using System;
using System.Globalization;
using Assets.Source.Common.WindowFsm;
using Assets.Source.Common.WindowFsm.Windows;
using DeadMosquito.AndroidGoodies;
using Source.Controllers.Api;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Sources.Infrastructure.Api.Services;

namespace Source.Controllers.Core.Presenters
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly IMainMenuView _mainMenuView;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;

        private DateTime _currentDateTime;

        public MainMenuPresenter(IMainMenuView mainMenuView, IWindowFsm windowFsm, ILogger logger)
        {
            _mainMenuView = mainMenuView ?? throw new ArgumentNullException(nameof(mainMenuView));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger;
        }

        public void Enable()
        {
            _currentDateTime = DateTime.Now;

            _mainMenuView.ViewTasksButton.Initialize();
            _mainMenuView.CreateTasksButton.Initialize();

            OnWindowOpened(_windowFsm.CurrentWindow);
            _windowFsm.Opened += OnWindowOpened;

            _mainMenuView.SetCurrentDateText(DateTime.Now.ToShortDateString());
            _mainMenuView.CreateTasksButton.Clicked += OnCreateTasksButtonClicked;
            _mainMenuView.ViewTasksButton.Clicked += OnViewTasksButtonClicked;
        }

        public void Disable()
        {
            _windowFsm.Opened -= OnWindowOpened;
            _mainMenuView.CreateTasksButton.Clicked -= OnCreateTasksButtonClicked;
            _mainMenuView.ViewTasksButton.Clicked -= OnViewTasksButtonClicked;
        }

        private void OnViewTasksButtonClicked()
        {
            AGDateTimePicker.ShowDatePicker(
                _currentDateTime.Year,
                _currentDateTime.Month,
                _currentDateTime.Day,
                OnDatePicked,
                OnDatePickCanceled);
        }

        private void OnCreateTasksButtonClicked()
        {
        }

        private void OnDatePicked(int year, int month, int day) =>
            _mainMenuView.SetCurrentDateText(new DateTime(year, month, day).ToString(CultureInfo.InvariantCulture));

        private void OnDatePickCanceled() =>
            _logger.LogWarning($"{nameof(MainMenuPresenter)}: {nameof(OnDatePickCanceled)}");

        private void OnWindowOpened(IWindow window)
        {
            if (window is RootWindow)
                _mainMenuView.Show();
            else
                _mainMenuView.Hide();
        }
    }
}