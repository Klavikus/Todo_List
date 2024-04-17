using System;
using System.Globalization;
using Modules.DAL.Implementation.Data.Entities;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Source.Presentation.Api.Views;
using UnityEngine;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Controllers.Core.Presenters
{
    public class TaskPresenter : IPresenter
    {
        private readonly ICreatedTaskView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly ILogger _logger;
        private readonly ITaskService _taskService;
        private readonly TaskData _taskData;

        public TaskPresenter(
            ICreatedTaskView view,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            TaskData taskData)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskData = taskData;
        }

        public void Enable()
        {
            _view.OpenViewButton.Initialize();
            _view.Name.text = _taskData.Name;
            _view.StatusImage.color = _taskData.IsCompleted ? Color.green : Color.red;
            _view.OpenViewButton.Clicked += OnOpenViewButtonClicked;
            _taskService.TaskChanged += OnTaskChanged;
            OnTaskChanged(_taskData);
        }

        private Color32 HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

            return new Color32(r, g, b, 255); // Альфа устанавливаем в максимальное значение, т.е. 255
        }

        private void OnTaskChanged(TaskData taskData)
        {
            if (taskData.Id != _taskData.Id)
                return;

            // 00FF38 good
            // FFAE00 bad

            _view.Name.text = _taskData.Name;

            if (_taskData.IsCompleted)
            {
                _view.SetCompleted();
                _view.StatusImage.color = HexToColor("00FF38");
            }
            else
            {
                _view.SetPending();
                _view.StatusImage.color = HexToColor("FFAE00");
            }
        }

        public void Disable()
        {
            _view.OpenViewButton.Clicked -= OnOpenViewButtonClicked;
            _taskService.TaskChanged -= OnTaskChanged;
        }

        private void OnOpenViewButtonClicked()
        {
            _taskService.FocusTask(_taskData);
            _windowFsm.OpenWindow<TaskWindow>();
        }
    }
}